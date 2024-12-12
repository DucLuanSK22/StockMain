using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stock.BE.Core.Enum;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace Stock.BE.Host.Controllers
{
    [Route("api/market")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private string _accessToken;
        private readonly string _apiUrl = "https://fc-data.ssi.com.vn/";
        private readonly string _consumerId = "d7704f486ea64f7986a84b1a35ac9495";
        private readonly string _consumerSecret = "cfb18ea2fded4a8f869eeff929a16c20";

        public MarketController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(50);
        }
        // Phương thức lấy access token
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
        [HttpGet("get_stock_by_period")]
        public async Task<IActionResult> GetStockByPeriodAsync(string indexId, PeriodEnum periodEnum)
        {
            try
            {
                string toDate = DateTime.Now.ToString("dd/MM/yyyy");
                string fromDate = "";
                switch (periodEnum)
                {
                    case PeriodEnum.Week1:
                        fromDate = DateTime.Now.AddDays(-8).ToString("dd/MM/yyyy");
                        break;
                    case PeriodEnum.Month1:
                        fromDate = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
                        break;
                }
                // Kiểm tra và lấy token nếu cần
                if (string.IsNullOrEmpty(_accessToken) && !await GetAccessTokenAsync())
                {
                    return StatusCode(500, "Unable to retrieve access token.");
                }

                var query = $"lookupRequest.indexId={indexId}&lookupRequest.fromDate={fromDate}&lookupRequest.toDate={toDate}&lookupRequest.pageIndex={1}&lookupRequest.pageSize={100}&lookupRequest.ascending={true}";
                var requestUrl = $"{_apiUrl}/api/v2/Market/DailyIndex?{query}";


                var response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Ok(data);
                }
                else
                {
                    return StatusCode(404, "Không tìm thấy dữ liệu");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpGet("get_popular_stock")]
        public async Task<IActionResult> GetDailyIndex()
        {
            string fromDate = DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy");
            string toDate = DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy");
            var indexIds = new List<string> { "VNINDEX", "VN30", "VNIT" };

            try
            {
                // Kiểm tra và lấy token nếu cần
                if (string.IsNullOrEmpty(_accessToken) && !await GetAccessTokenAsync())
                {
                    return StatusCode(500, "Unable to retrieve access token.");
                }

                var result = new List<string>();
                foreach (var index in indexIds)
                {
                    var query = $"lookupRequest.indexId={index}&lookupRequest.fromDate={fromDate}&lookupRequest.toDate={toDate}&lookupRequest.pageIndex={1}&lookupRequest.pageSize={100}&lookupRequest.ascending={true}";
                    var requestUrl = $"{_apiUrl}/api/v2/Market/DailyIndex?{query}";


                    var response = await _httpClient.GetAsync(requestUrl);
                    await Task.Delay(1000);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        result.Add(data);

                    }
                }
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpGet("DailyStockPrice")]
        public async Task<IActionResult> GetDailyStockPrice([FromQuery] string symbol = "MBB", [FromQuery] string market = "HOSE", [FromQuery] string fromDate = "01/11/2024", [FromQuery] string toDate = null, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 1000)
        {
            // Thiết lập toDate mặc định là ngày hiện tại nếu không được cung cấp
            toDate ??= DateTime.Now.ToString("dd/MM/yyyy");

            // Xây dựng URL với các tham số truy vấn
            var query = $"lookupRequest.symbol={symbol}&lookupRequest.fromDate={fromDate}&lookupRequest.toDate={toDate}&lookupRequest.pageIndex={pageIndex}&lookupRequest.pageSize={pageSize}&lookupRequest.market={market}";
            var requestUrl = $"{_apiUrl}/api/v2/Market/DailyStockPrice?{query}";

            try
            {
                // Kiểm tra và lấy token nếu cần
                if (string.IsNullOrEmpty(_accessToken))
                {
                    var tokenRetrieved = await this.GetAccessTokenAsync();
                    if (!tokenRetrieved)
                    {
                        return StatusCode(500, "Unable to retrieve access token.");
                    }
                }
                // Thực hiện yêu cầu GET
                var response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Content(data, "application/json");
                }
                else
                {
                    Console.WriteLine("API Error: " + response.StatusCode);
                    return StatusCode((int)response.StatusCode, "Error fetching data from the API.");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("API Error: " + ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpGet("Securities")]
        public async Task<IActionResult> GetSecurities([FromQuery] int pageIndex = 4, [FromQuery] int pageSize = 100, [FromQuery] string market = "HOSE")
        {
            // Xây dựng URL với các tham số truy vấn
            var query = $"lookupRequest.market={market}&lookupRequest.pageIndex={pageIndex}&lookupRequest.pageSize={pageSize}";
            var requestUrl = $"{_apiUrl}/api/v2/Market/Securities?{query}";

            try
            {
                // Kiểm tra và lấy token nếu cần
                if (string.IsNullOrEmpty(_accessToken))
                {
                    var tokenRetrieved = await this.GetAccessTokenAsync();
                    if (!tokenRetrieved)
                    {
                        return StatusCode(500, "Unable to retrieve access token.");
                    }
                }
                var response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Content(data, "application/json");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error fetching data from the API.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("SecuritiesDetails")]
        public async Task<IActionResult> GetSecuritiesDetails([FromQuery] string market = "DER", [FromQuery] string symbol = "", [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 1000)
        {
            var query = $"lookupRequest.market={market}&lookupRequest.symbol={symbol}&lookupRequest.pageIndex={pageIndex}&lookupRequest.pageSize={pageSize}";
            var requestUrl = $"{_apiUrl}/api/v2/Market/SecuritiesDetails?{query}";

            try
            {
                // Kiểm tra và lấy token nếu cần
                if (string.IsNullOrEmpty(_accessToken))
                {
                    var tokenRetrieved = await this.GetAccessTokenAsync();
                    if (!tokenRetrieved)
                    {
                        return StatusCode(500, "Unable to retrieve access token.");
                    }
                }
                var response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return Content(data, "application/json");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error fetching data from the API.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
