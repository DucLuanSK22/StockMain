using Stock.BE.Core.BaseDL.Repository;
using Stock.BE.Core.Entity;
using Stock.BE.Core.Enum;
using Stock.BE.Core.Model;
using System.Threading.Tasks;

namespace Stock.BE.Core.DL
{
    public interface IStockDL : IBaseRepository<StockEntity>
    {
        /// <summary>
        /// Hàm cộng tiền vào tài khoản của người dùng
        /// </summary>
        /// <param name="userId">ID của người dùng được nạp tiền vào tài khoản</param>
        /// <returns></returns>
        /// Created by: nttue - 12.10.2024
        Task AddCashByUser(Guid userId);

        Task<List<TableAssetHistoryModel>> GetAssetHistoryByUserAsync(Guid userId, PeriodEnum periodEnum);

        /// <summary>
        /// Hàm thêm mới giao dịch trên hệ thống khi người mua đặt lệnh mua hoặc bán
        /// </summary>
        /// <param name="transactionsDto">Giao dịch</param>
        /// <returns></returns>
        /// Created by: nttue - 12.10.2024
        Task<AddTransactionResult> InsertTransaction(TransactionsDTO transactionsDto);
        Task<List<object>> GetTransactionsByUserAsync(Guid userId);
        Task<List<DealModel>> GetDealsByUserAsync(Guid userId);

        Task<object?> GetAssetDashboard(Guid userId);
        Task<List<string>> GetAllStockCodeAsync();

        Task UpdateStockAsync(string sql, object param);
        Task<Dictionary<string, List<StockEntity>>> GetReportStockAsync();

        Task UpdateAssetUserAsync();
    }
}
