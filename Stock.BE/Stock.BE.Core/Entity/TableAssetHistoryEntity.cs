namespace Stock.BE.Core.Entity;
public class TableAssetHistoryEntity
{
    public Guid table_asset_history_id { get; set; }
    public Guid user_id { get; set; }
    public double total_net_assets { get; set; } = 0;
    public double stock_value { get; set; } = 0;
    public double cash_value { get; set; } = 0;
    public DateTime created_at { get; set; } = DateTime.Now;
}
