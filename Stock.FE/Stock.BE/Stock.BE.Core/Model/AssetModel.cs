namespace Stock.BE.Core.Model;
public class AssetModel
{
    /// <summary>
    /// Tổng tài sản của người dùng
    /// </summary>
    public double total_asset { get; set; }
    /// <summary>
    /// Tài sản người dùng thay đổi theo ngày 
    /// </summary>
    public double change_asset { get; set; }
}
