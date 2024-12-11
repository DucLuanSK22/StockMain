namespace Stock.BE.Core.Model;

public class StockDataModel
{
    public string TradingDate { get; set; }
    public decimal PriceChange { get; set; }
    public decimal PerPriceChange { get; set; }
    public decimal CeilingPrice { get; set; }
    public decimal FloorPrice { get; set; }
    public decimal RefPrice { get; set; }
    public decimal OpenPrice { get; set; }
    public decimal HighestPrice { get; set; }
    public decimal LowestPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal AveragePrice { get; set; }
    public decimal ClosePriceAdjusted { get; set; }
    public long TotalMatchVol { get; set; }
    public decimal TotalMatchVal { get; set; }
    public decimal TotalDealVal { get; set; }
    public long TotalDealVol { get; set; }
    public long ForeignBuyVolTotal { get; set; }
    public long ForeignCurrentRoom { get; set; }
    public long ForeignSellVolTotal { get; set; }
    public decimal ForeignBuyValTotal { get; set; }
    public decimal ForeignSellValTotal { get; set; }
    public long TotalBuyTrade { get; set; }
    public long TotalBuyTradeVol { get; set; }
    public long TotalSellTrade { get; set; }
    public long TotalSellTradeVol { get; set; }
    public long NetBuySellVol { get; set; }
    public decimal NetBuySellVal { get; set; }
    public long TotalTradedVol { get; set; }
    public decimal TotalTradedValue { get; set; }
    public string Symbol { get; set; }
    public DateTime? Time { get; set; }
}

public class ResultModel
{
    public List<StockDataModel> data { get; set; } = new List<StockDataModel>();
}