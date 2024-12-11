using Stock.BE.Core.BaseDL.Repository;
using Stock.BE.Core.Entity;
using Stock.BE.Core.Enum;

namespace Stock.BE.Core.DL
{
    public interface IStockDL : IBaseRepository<StockEntity>
    {
        Task<List<object>> GetStockByPeriodAsync(Guid stockId, PeriodEnum periodEnum);
    }
}
