namespace Stock.BE.Core.Model;
public class TableAssetHistoryModel
{
    public Guid table_asset_history_id { get; set; }
    public Guid? user_id { get; set; }
    public decimal? total_net_assets { get; set; }
    public decimal? stock_value { get; set; }
    public decimal? cash_value { get; set; }
    public DateTime? created_at { get; set; }
}
