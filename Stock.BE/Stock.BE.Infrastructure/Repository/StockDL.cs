using Dapper;
using Stock.BE.Core;
using Stock.BE.Core.DL;
using Stock.BE.Core.Entity;
using Stock.BE.Core.Enum;
using Stock.BE.Core.Model;
using Stock.BE.Infrastructure.Repository.Base;
using static Dapper.SqlMapper;

namespace Stock.BE.Infrastructure.Repository
{
    public class StockDL : BaseRepository<StockEntity>, IStockDL
    {
        public StockDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        /// <summary>
        /// Đầu lấy dữ liệu sự thay đổi của các mã theo từng khoảng thời gain
        /// </summary>
        /// <param name="stockId">ID của chứng khoán</param>
        /// <param name="periodEnum">Kỳ thay đổi</param>
        /// <returns></returns>
        public async Task<List<object>> GetStockByPeriodAsync(Guid stockId, PeriodEnum periodEnum)
        {
            var param = new Dictionary<string, object>
                {
                    { "@stockId", stockId }
                };
            string sql = string.Empty;
            switch (periodEnum)
            {
                case PeriodEnum.Week1:
                    sql = "SELECT * FROM stock_price_changes WHERE stock_id = @stockId AND modified_at BETWEEN NOW() - INTERVAL '1 week' AND NOW();";
                    break;
                case PeriodEnum.Month1:
                    sql = "SELECT * FROM stock_price_changes WHERE stock_id = @stockId AND modified_at BETWEEN NOW() - INTERVAL '1 month' AND NOW();";
                    break;
                case PeriodEnum.Month6:
                    sql = "SELECT * FROM stock_price_changes WHERE stock_id = @stockId AND modified_at BETWEEN NOW() - INTERVAL '6 month' AND NOW();";
                    break;
                default:
                    sql = "SELECT * FROM stock_price_changes WHERE stock_id = @stockId AND modified_at BETWEEN NOW() - INTERVAL '1 year' AND NOW();";
                    break;
            }
            var result = await _uow.QueryList<object>(sql, param);
            return result;
        }
        /// <summary>
        /// Lấy lên các chứng khoán phổ biến
        /// </summary>
        /// <returns></returns>
        public async Task<List<StockEntity>> GetPopularStockAsync()
        {
            var sql = @"select * from stock where stock_id in ('ac29582d-e41a-4576-9596-4ac398f14f84',
                                            '9db887ad-1c70-479e-87c6-4bdcabf71d4e',
                                            '75f14796-133c-4691-aa3d-e31a7f290c91')";
            var result = await _uow.Connection.QueryAsync<StockEntity>(sql);
            return result.ToList();
        }
        /// <summary>
        /// Lấy tài sản và sự thay đổi tài sản của người dùng theo ngày
        /// </summary>
        /// <param name="userId">Id của người dùng</param>
        /// <returns></returns>
        public async Task<AssetModel> GetAssetByUserAsync(Guid userId)
        {
            var param = new DynamicParameters();
            param.Add("@userId", userId);

            var sql = @"select total_net_assets from ""user"" u where user_id = @userId; 
                                    select sum(profit_loss) from deals d where user_id = @userId;";

            using (var multi = await _uow.Connection.QueryMultipleAsync(sql, param))
            {
                var data1 = await multi.ReadAsync<double>();
                var data2 = await multi.ReadAsync<double>();

                var result = new AssetModel
                {
                    total_asset = data1.FirstOrDefault(),
                    change_asset = data2.FirstOrDefault()
                };

                return result;
            }
        }
        /// <summary>
        /// Đầu lấy ra thông tin các deal nắm giữ của người dùng
        /// </summary>
        /// <param name="userId">Id của người dùng</param>
        /// <returns></returns>
        public async Task<object> GetDealsByUserAsync(Guid userId)
        {
            var param = new DynamicParameters();
            param.Add("@userId", userId);

            var sql = @"select * from deals d where user_id = @userId;";
            var result = await _uow.QueryList<object>(sql, param);
            return result;
        }
        /// <summary>
        /// Đầu lấy các giao dịch của người dùng
        /// </summary>
        /// <param name="userId">Id của người dùng</param>
        /// <returns></returns>
        public async Task<object> GetTransactionsByUserAsync(Guid userId)
        {
            var param = new DynamicParameters();
            param.Add("@userId", userId);
            var sql = @"select * from transactions d where user_id = @userId;";
            var result = await _uow.QueryList<object>(sql, param);
            return result;
        }
        /// <summary>
        /// Đầu lấy thông tin của người dùng bao gồm luôn cả tài sản
        /// </summary>
        /// <param name="userId">Id của người dùng</param>
        /// <returns></returns>
        public async Task<object> GetUserByIdAsync(Guid userId)
        {
            var param = new DynamicParameters();
            param.Add("@userId", userId);
            var sql = @"select * from user d where user_id = @userId;";
            var result = await _uow.QueryList<object>(sql, param);
            return result;
        }
        /// <summary>
        /// Đầu thêm tiền mặt cho người dùng
        /// </summary>
        /// <param name="userId">Id của người dùng</param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task AddCashByUser(Guid userId, int amount)
        {
            var param = new DynamicParameters();
            param.Add("@userId", userId);
            param.Add("@amount", amount);
            var sql = @"update user set cash_value = @amount where user_id = @userId";
            await _uow.Connection.ExecuteAsync(sql, param);
        }
        /// <summary>
        /// Đầu thêm các deal nắm giữ của người dùng
        /// </summary>
        /// <returns></returns>
        public async Task InsertDeal(DealDTO dealDTO)
        {
            var deal = new DealEntity
            {
                deal_id = Guid.NewGuid(),
                stock_id = dealDTO.stock_id,
                stock_code = dealDTO.stock_code,
                total_volume = dealDTO.total_volume,
                total_tradeable_volume = dealDTO.total_tradeable_volume,
                market_value = dealDTO.market_value,
                matched_price = dealDTO.matched_price,
                current_price = dealDTO.current_price,
                cost_value = dealDTO.cost_value,
                profit_loss = dealDTO.profit_loss,
                profit_loss_by_percent = dealDTO.profit_loss_by_percent
            };
            await base.BaseInsertAsync(deal, "deals");
        }
        /// <summary>
        /// Đầu thêm các giao dịch mua bán của người dùng
        /// </summary>
        /// <returns></returns>
        public async Task InsertTransaction(TransactionsDTO transactionsDto)
        {
            var transaction = new TransactionsEntity
            {
                transaction_id = Guid.NewGuid(),
                stock_id = transactionsDto.stock_id,
                user_id = transactionsDto.user_id,
                stock_code = transactionsDto.stock_code,
                transaction_type = transactionsDto.transaction_type,
                created_at = DateTime.Now,
                order_price = transactionsDto.order_price,
                matched_price = transactionsDto.matched_price,
                volume = transactionsDto.volume,
            };
             await base.BaseInsertAsync(transaction, "transactions");
        }
    }
}
