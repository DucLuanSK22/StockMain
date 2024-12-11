namespace Stock.BE.Core.Model;
public class TransactionModel
{
    public Guid transactions_id { get; set; }
    public Guid stock_id { get; set; }
    public Guid user_id { get; set; }
    public decimal matched_price { get; set; }
    public int volume { get; set; }
    /// <summary>
    /// Cờ để kiểm tra lệnh mua hết hay bán hết
    /// </summary>
    public bool done { get; set; } = false;
    /// <summary>
    /// Khối lượng còn lại sau khi thực hiện giao dịch
    /// </summary>
    public int rest { get; set; }
}
