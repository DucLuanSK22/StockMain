namespace Stock.BE.Core.Entity;
public class TransactionsEntity
{
    /// <summary>
    /// Mã giao dịch
    /// </summary>
    public Guid transactions_id { get; set; }

    /// <summary>
    /// Mã chứng khoán
    /// </summary>
    public Guid stock_id { get; set; }

    /// <summary>
    /// Mã người dùng
    /// </summary>
    public Guid user_id { get; set; }

    /// <summary>
    /// Mã chứng khoán (ký hiệu)
    /// </summary>
    public string stock_code { get; set; }

    /// <summary>
    /// Loại giao dịch (1: mua, 2: bán, ...)
    /// </summary>
    public int transaction_type { get; set; }

    /// <summary>
    /// Thời gian tạo giao dịch
    /// </summary>
    public DateTime created_at { get; set; }

    /// <summary>
    /// Giá đặt hàng
    /// </summary>
    public decimal order_price { get; set; }

    /// <summary>
    /// Giá khớp lệnh
    /// </summary>
    public decimal matched_price { get; set; }

    /// <summary>
    /// Số lượng giao dịch
    /// </summary>
    public int volume { get; set; }

    /// <summary>
    /// Trạng thái giao dịch (1: thành công, 2: thất bại, ...)
    /// </summary>
    public int status { get; set; }
}

