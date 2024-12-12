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
    public string stock_code { get; set; } = string.Empty;
    /// <summary>
    /// ID của người dùng
    /// </summary>
    public Guid user_id { get; set; }

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
}
 