using Stock.BE.Core.BaseDL.Repository;
using Stock.BE.Core.Entity;
using Stock.BE.Core.Enum;
using Stock.BE.Core.Model;

namespace Stock.BE.Core.DL
{
    public interface IStockDL : IBaseRepository<StockEntity>
    {
        Task<List<object>> GetStockByPeriodAsync(Guid stockId, PeriodEnum periodEnum);

        Task<List<StockEntity>> GetPopularStockAsync();
        /// <summary>
        /// Hàm cập nhật giá trị của mã chứng khoán, tài sản chứng khoán của người dùng
        ///  và thực hiện insert dữ liệu với bảng lịch sử thay đổi giá chứng khoán
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue - 12.10.2024
        Task UpdateStockPriceChange();
        /// <summary>
        /// Hàm cộng tiền vào tài khoản của người dùng
        /// </summary>
        /// <param name="userId">ID của người dùng được nạp tiền vào tài khoản</param>
        /// <returns></returns>
        /// Created by: nttue - 12.10.2024
        Task AddCashByUser(Guid userId);

        Task<List<TableAssetHistoryModel>> GetAssetHistoryByUserAsync(Guid userId, PeriodEnum periodEnum);
    }
}
