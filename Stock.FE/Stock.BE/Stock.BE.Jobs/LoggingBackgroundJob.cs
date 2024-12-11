using Newtonsoft.Json;
using Quartz;
using Stock.BE.Core.DL;
using Stock.BE.Core.Model;
using System.Net.Http.Headers;
using System.Text;
namespace Stock.BE.Jobs;

[DisallowConcurrentExecution]
public class LoggingBackgroundJob : IJob
{
    private readonly IStockDL _stockDL;
    private readonly HttpClient _httpClient;
    private string _accessToken;
    private readonly string _apiUrl = "https://fc-data.ssi.com.vn/";
    private readonly string _consumerId = "d7704f486ea64f7986a84b1a35ac9495";
    private readonly string _consumerSecret = "cfb18ea2fded4a8f869eeff929a16c20";
    public LoggingBackgroundJob(IStockDL stockDL, IHttpClientFactory httpClientFactory)
    {
        _stockDL = stockDL;
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(50);
    }
    public async Task Execute(IJobExecutionContext context)
    {
        await this.HandleData();
    }

    private async Task HandleData()
    {
        var stockCodes = await _stockDL.GetAllStockCodeAsync();
        if (stockCodes.Count > 0)
        {
            foreach (string stockCode in stockCodes)
            {
                await this.UpdateStock(stockCode);
            }

        }
    }
    private async Task<bool> GetAccessTokenAsync()
    {
        var requestUrl = $"{_apiUrl}/api/v2/Market/AccessToken";

        var requestData = new
        {
            consumerID = _consumerId,
            consumerSecret = _consumerSecret
        };
        var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
        try
        {
            var response = await _httpClient.PostAsync(requestUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseBody);

                if (result.status == 200)
                {
                    _accessToken = result.data.accessToken;
                    // Thiết lập `Authorization` header cho tất cả các yêu cầu
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to get token: " + result.message);
                }
            }
            else
            {
                Console.WriteLine("HTTP error: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }

        return false;
    }

    public async Task UpdateStock(string symbol)
    {
        // Thiết lập toDate mặc định là ngày hiện tại nếu không được cung cấp
        string toDate = DateTime.Now.ToString("dd/MM/yyyy");
        string fromDate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
        // Xây dựng URL với các tham số truy vấn
        var query = $"lookupRequest.symbol={symbol}&lookupRequest.fromDate={fromDate}&lookupRequest.toDate={toDate}&lookupRequest.pageIndex={1}&lookupRequest.pageSize={1000}&lookupRequest.market=HOSE";
        var requestUrl = $"{_apiUrl}/api/v2/Market/DailyStockPrice?{query}";
        Console.WriteLine(symbol);
        try
        {
            // Kiểm tra và lấy token nếu cần

            if (string.IsNullOrEmpty(_accessToken))
            {
                var tokenRetrieved = await this.GetAccessTokenAsync();
            }
            // Thực hiện yêu cầu GET
            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResultModel>(data);

                if (result != null && result.data != null && result.data.Count > 0)
                {
                    var entity = result.data[0];
                    var sql = $@"UPDATE public.stocks
                                                    SET max_price = @max_price, min_price = @min_price, reference_price = @reference_price, matched_price = @matched_price, total_volume = @total_volume, total_assets = @total_assets, difference = @difference, change_price = @change_price, per_price_change = @per_price_change, tradable_volume = @tradable_volume, modified_date = @modified_date
                                                    WHERE stock_code  =  @stock_code;";
                    var param = new Dictionary<string, object>
                    {
                         { $"@max_price", entity.CeilingPrice / 1000 },
                         { $"@min_price", entity.FloorPrice  / 1000 },
                         { $"@reference_price", entity.RefPrice / 1000 },
                         { $"@matched_price",entity.RefPrice  / 1000 },
                         { $"@total_volume", entity.TotalMatchVol },
                         { $"@total_assets",entity.TotalMatchVal },
                         { $"@difference",entity.PriceChange > 0 ? 0 : (entity.PriceChange < 0 ? 1:2 ) },
                         { $"@change_price", entity.PriceChange  / 1000 },
                         { $"@per_price_change", entity.PerPriceChange },
                         { $"@tradable_volume",entity.TotalTradedVol },
                         { $"@modified_date", DateTime.Now },
                           { $"@stock_code", entity.Symbol },
                    };
                    await _stockDL.UpdateStockAsync(sql, param);
                }
            }

        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("API Error: " + ex.Message);
        }
    }
}