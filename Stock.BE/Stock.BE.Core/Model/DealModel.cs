namespace Stock.BE.Core.Model;
public class DealModel
{
    public Guid deal_id { get; set; }
    public Guid stock_id { get; set; }
    public string stock_code { get; set; } = string.Empty;
    public int total_volume { get; set; }
    public int total_tradeable_volume { get; set; }
    public decimal matched_price { get; set; }
    public decimal current_price { get; set; }
    public decimal cost_value { get; set; }
    public decimal profit_loss { get; set; }
    public decimal profit_loss_by_percent { get; set; }
    public decimal difference { get; set; }
    public int is_profit { get; set; }
}
