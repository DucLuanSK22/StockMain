namespace Stock.BE.Core.Entity;
/// <summary>
/// Lớp đại diện cho một giao dịch (deal)
/// </summary>
public class DealEntity
{
    /// <summary>
    /// ID của giao dịch
    /// </summary>
    public Guid deal_id { get; set; }

    /// <summary>
    /// ID của cổ phiếu
    /// </summary>
    public Guid stock_id { get; set; }

    /// <summary>
    /// ID của người dùng
    /// </summary>
    public Guid user_id { get; set; }

    /// <summary>
    /// Mã cổ phiếu
    /// </summary>
    public string stock_code { get; set; }

    /// <summary>
    /// Tổng số lượng giao dịch
    /// </summary>
    public int total_volume { get; set; }

    /// <summary>
    /// Tổng số lượng giao dịch có thể giao dịch
    /// </summary>
    public int total_tradeable_volume { get; set; }

    /// <summary>
    /// Giá khớp lệnh
    /// </summary>
    public decimal matched_price { get; set; }

    /// <summary>
    /// Giá hiện tại
    /// </summary>
    public decimal current_price { get; set; }

    /// <summary>
    /// Giá trị chi phí
    /// </summary>
    public decimal cost_value { get; set; }

    /// <summary>
    /// Giá trị thị trường
    /// </summary>
    public decimal market_value { get; set; }

    /// <summary>
    /// Lãi/lỗ
    /// </summary>
    public decimal profit_loss { get; set; }

    /// <summary>
    /// Lãi/lỗ theo phần trăm
    /// </summary>
    public decimal profit_loss_by_percent { get; set; }
}
