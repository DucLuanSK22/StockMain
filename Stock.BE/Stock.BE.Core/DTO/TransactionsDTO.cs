namespace Stock.BE.Core;
public class TransactionsDTO
{
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

    /// <summary>
    /// Thời gian giao dịch thay đổi
    /// </summary>
    public DateTime modified_at { get; set; } = DateTime.Now;
}

