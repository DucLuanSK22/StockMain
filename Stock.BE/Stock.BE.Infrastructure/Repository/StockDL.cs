using Dapper;
using Newtonsoft.Json;
using Stock.BE.Core;
using Stock.BE.Core.DL;
using Stock.BE.Core.Entity;
using Stock.BE.Core.Enum;
using Stock.BE.Core.Model;
using Stock.BE.Email;
using Stock.BE.Infrastructure.Repository.Base;
using System.Data;
using System.Runtime.InteropServices;
using static Dapper.SqlMapper;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace Stock.BE.Infrastructure.Repository
{
    public class StockDL : BaseRepository<StockEntity>, IStockDL
    {
        private readonly IEmail _email;
        private readonly IUserDL _userDL;
        public StockDL(IUnitOfWork unitOfWork, IEmail email, IUserDL userDL) : base(unitOfWork)
        {
            _email = email;
            _userDL = userDL;
        }
        /// <summary>
        /// Đầu lấy ra thông tin các deal nắm giữ của người dùng
        /// </summary>
        /// <param name="userId">Id của người dùng</param>
        /// <returns></returns>
        public async Task<List<DealModel>> GetDealsByUserAsync(Guid userId)
        {
            var param = new DynamicParameters();
            param.Add("@userId", userId);

            var sql = @"
                                select 
                                    d.deal_id,
                                    d.stock_id,
                                    s.stock_code,
                                    d.total_volume, 
                                    s.tradable_volume as total_tradeable_volume,
                                    d.matched_price, 
                                    s.matched_price as current_price,
                                    CAST(d.matched_price * d.total_volume * 1000 AS decimal) as cost_value,
                                    CAST((s.matched_price - d.matched_price) * d.total_volume * 1000 AS decimal) as profit_loss,
                                    CAST(((s.matched_price - d.matched_price) * d.total_volume * 1000) 
                                         / (d.matched_price * d.total_volume * 1000) * 100 AS decimal) as profit_loss_by_percent,
                                    s.difference,
                                    case 
                                        when s.matched_price - d.matched_price > 0 then 0
                                        else 1 
                                    end as is_profit
                                from 
                                    deals d 
                                join stocks s on s.stock_id = d.stock_id
                                where d.user_id = @userId;
                            ";

            var result = await _uow.QueryList<DealModel>(sql, param);
            return result;
        }
        /// <summary>
        /// Đầu lấy các giao dịch của người dùng
        /// </summary>
        /// <param name="userId">Id của người dùng</param>
        /// <returns></returns>
        public async Task<List<object>> GetTransactionsByUserAsync(Guid userId)
        {
            var param = new DynamicParameters();
            param.Add("@userId", userId);
            var sql = @"select * from transactions d where user_id = @userId;";
            var result = await _uow.QueryList<object>(sql, param);
            return result;
        }
        /// <summary>
        /// Đầu thêm tiền mặt cho người dùng
        /// </summary>
        /// <param name="userId">Id của người dùng</param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task AddCashByUser(Guid userId)
        {
            var paramDictionary = new Dictionary<string, object>()
            {
                {"p_user_id", userId }
            };
            var param = new DynamicParameters(paramDictionary);
            var sql = "select * from public.update_cash_value_by_user(@p_user_id)";
            await _uow.Connection.ExecuteAsync(sql, param, commandType: CommandType.Text);

        }
        /// <summary>
        /// Đầu thêm các giao dịch mua bán của người dùng
        /// </summary>
        /// <returns></returns>
        public async Task<AddTransactionResult> InsertTransaction(TransactionsDTO transactionsDto)
        {
            var result = new AddTransactionResult();
            var transaction = new TransactionsEntity
            {
                transactions_id = Guid.NewGuid(),
                stock_id = transactionsDto.stock_id,
                user_id = transactionsDto.user_id,
                stock_code = transactionsDto.stock_code,
                transaction_type = transactionsDto.transaction_type,
                created_at = DateTime.Now,
                order_price = transactionsDto.order_price,
                matched_price = transactionsDto.matched_price,
                volume = transactionsDto.volume,
            };
            if (transaction.transaction_type == 0)
            {
                await this.BuyStock(transaction);
            }
            else
            {
                var obj = await this.GetTotalVolumeByUserId(transaction.user_id);
                if (obj == null || transaction.volume > obj?.total_volume)
                {
                    result.Success = false;
                    result.Message = "Số lượng bán không được phép lớn hơn số lượng sở hữu hiện có ";
                }
                else
                {
                    await this.SellStock(transaction, transaction.volume == obj?.total_volume, obj?.deal_id);
                }
            }

            await this.UpdateDealAsync(transaction.stock_id);
            return result;
        }
        /// <summary>
        /// Hàm thực hiện lệnh mua mã chứng khoán
        /// </summary>
        /// <param name="transactionsEntity">Đối tượng giao dịch</param>
        /// <returns></returns>
        /// <exception cref="Exception">Exception trong quá trình thực hiện giao dịch</exception>
        /// Created by: nttue (13/11/2024)
        private async Task BuyStock(TransactionsEntity transactionsEntity)
        {
            try
            {
                // Lấy lên các lệnh bán khớp với lệnh mua
                var param = new Dictionary<string, object>
                {
                    { $"@stock_id", transactionsEntity.stock_id },
                    { $"@order_price", transactionsEntity.order_price }
                };

                var sql = @$"select 
                                                t.transactions_id,
                                                t.stock_id,
                                                t.user_id, 
                                                t.order_price as matched_price, 
                                                t.volume
                                            from 
                                                transactions t
                                            where transaction_type = 1 and ""status"" = 0 and t.stock_id = @stock_id and t.order_price <= @order_price
                                            order by created_at desc 
                                            ;";

                var sells = await _uow.QueryList<TransactionModel>(sql, param);

                if (sells.Count > 0)
                {
                    decimal amounts = 0;
                    int volumes = 0;
                    int startVolume = transactionsEntity.volume;
                    for (int i = 0; i < sells.Count; i++)
                    {
                        var sell = sells[i];
                        if (startVolume >= sell.volume)
                        {
                            startVolume -= sell.volume;
                            sell.done = true;
                            sell.rest = 0;
                            amounts += sell.matched_price * sell.volume;
                            volumes += sell.volume;
                        }
                        else
                        {
                            sell.rest = sell.volume - startVolume;
                            amounts += sell.matched_price * startVolume;
                            volumes += startVolume;
                            startVolume = 0;
                        }
                    }
                    var stock = await this.GetByIdAsync(transactionsEntity.stock_id);
                    // Nếu mua thành công thì thêm deal của người dùng
                    if (stock != null)
                    {
                        var newDeal = new DealEntity
                        {
                            deal_id = Guid.NewGuid(),
                            stock_id = transactionsEntity.stock_id,
                            stock_code = stock.stock_code,
                            user_id = transactionsEntity.user_id,
                            total_volume = volumes,
                            total_tradeable_volume = stock.tradable_volume,
                            matched_price = Math.Round(amounts / volumes, 2)
                        };
                        await base.BaseInsertAsync(newDeal, "deals");
                    }

                    // Cập nhật các lệnh bán
                    foreach (var sell in sells)
                    {
                        // Nếu thực hiện bán hết thành công thực hiện cập nhật trạng thái  của lệnh bán về thành công
                        var paramTran = new Dictionary<string, object>
                             {
                                    { $"@transactions_id", sell.transactions_id}
                             };
                        if (sell.done)
                        {
                            string sqlDeleteTran = @$"update transactions set status = 1 where transactions_id = @transactions_id;";
                            await _uow.ExecuteDefault(sqlDeleteTran, paramTran);
                        }
                        // Nếu chỉ thực hiện bán được 1 phần thì tạo ra 1 lệnh bán thành công và cập nhật lại số lượng lệnh bán cũ 
                        else
                        {
                            if (stock != null && sell.rest < sell.volume)
                            {
                                var newTran = new TransactionsEntity
                                {
                                    transactions_id = Guid.NewGuid(),
                                    stock_id = sell.stock_id,
                                    user_id = sell.user_id,
                                    stock_code = stock.stock_code,
                                    transaction_type = 1,
                                    created_at = DateTime.Now,
                                    order_price = sell.matched_price,
                                    matched_price = stock.matched_price,
                                    volume = sell.volume - sell.rest,
                                    status = 1
                                };
                                await base.BaseInsertAsync(newTran, "transactions");
                            }

                            paramTran.Add("@volume", sell.rest);
                            string sqlUpdateTran = @$"update transactions set volume = @volume where transactions_id = @transactions_id;";
                            await _uow.ExecuteDefault(sqlUpdateTran, paramTran);
                        }
                        if (stock != null)
                        {
                            await this.SendEmail(stock.stock_code, sell.user_id, sell.volume - sell.rest, "Bán");
                        }
                        // Sau khi bán thành công tiến hành cập nhật lại tài sản của người bán
                        // Cập nhật lại thay đổi số tiền của người dùng                        
                        var user1 = await this.UpdateAssetUserAsync(sell.user_id, (sell.volume - sell.rest) * sell.matched_price, "+");
                        await this.InsertTableAssetHistoryAsync(user1);
                    }

                    // Tiến hành cập nhật lại các lệnh mua và tài sản của người mua
                    if (startVolume == 0)
                    { // Nếu người mua mua được hết khối lượng đặt => cập nhật trạng thái giao dịch về thành công
                        transactionsEntity.status = 1;
                        await base.BaseInsertAsync(transactionsEntity, "transactions");
                    }
                    else
                    {
                        // Nếu chỉ mua được 1 phần thì vẫn thêm giao dịch nhưng khối lượng đặt bằng khối lượng chưa mua được 
                        transactionsEntity.status = 0;
                        transactionsEntity.volume = startVolume;
                        await base.BaseInsertAsync(transactionsEntity, "transactions");
                    }
                    // Cập nhật lại thay đổi số tiền của người dùng                    
                    var user = await this.UpdateAssetUserAsync(transactionsEntity.user_id, volumes * amounts / 1000 + (transactionsEntity.volume - volumes) * transactionsEntity.order_price);
                    await this.InsertTableAssetHistoryAsync(user);
                }
                else
                {
                    var matchStocks = await this.GetStocksForBuy(transactionsEntity.order_price, transactionsEntity.stock_id);
                    if (matchStocks != null && matchStocks.Count > 0)
                    {
                        var matchStock = matchStocks[0];
                        transactionsEntity.status = 1;
                        var newDeal = new DealEntity
                        {
                            deal_id = Guid.NewGuid(),
                            stock_id = transactionsEntity.stock_id,
                            stock_code = matchStock.stock_code,
                            user_id = transactionsEntity.user_id,
                            total_volume = transactionsEntity.volume,
                            total_tradeable_volume = matchStock.tradable_volume,
                            matched_price = matchStock.matched_price
                        };
                        await base.BaseInsertAsync(newDeal, "deals");
                    }
                    else
                    {
                        transactionsEntity.status = 0;
                    }
                    await base.BaseInsertAsync(transactionsEntity, "transactions");
                    var user = await this.UpdateAssetUserAsync(transactionsEntity.user_id, transactionsEntity.volume * transactionsEntity.order_price);
                    await this.InsertTableAssetHistoryAsync(user);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<StockEntity>> GetStocksForBuy(decimal matchPrice, Guid stockId)
        {
            var param = new Dictionary<string, object>
                {
                    { $"@stock_id", stockId },
                    { $"@match_price", matchPrice }
                };
            string sql = "select * from stocks s where stock_id = @stock_id and matched_price <= @match_price;";
            var result = await _uow.QueryList<StockEntity>(sql, param);
            return result;
        }

        /// <summary>
        /// Hàm thực hiện lệnh bán mã chứng khoán
        /// </summary>
        /// <param name="transactionsEntity">Đối tượng giao dịch</param>
        /// <returns></returns>
        /// <exception cref="Exception">Exception trong quá trình thực hiện giao dịch</exception>
        /// Created by: nttue (13/11/2024)
        private async Task SellStock(TransactionsEntity transactionsEntity, bool sellFull = false, Guid? dealId = null)
        {
            try
            {
                // Lấy lên các lệnh bán khớp với lệnh mua
                var param = new Dictionary<string, object>
                {
                    { $"@stock_id", transactionsEntity.stock_id },
                    { $"@order_price", transactionsEntity.order_price }
                };

                var sql = @$"select 
                                                t.transactions_id,
                                                t.stock_id,
                                                t.user_id, 
                                                t.order_price as matched_price, 
                                                t.volume
                                            from 
                                                transactions t
                                            where transaction_type = 0 and ""status"" = 0 and t.stock_id = @stock_id and t.order_price >= @order_price
                                            order by created_at desc 
                                            ;";

                var buys = await _uow.QueryList<TransactionModel>(sql, param);
                // Nếu bán hết deal nắm giữ => xóa deal
                if (dealId != null)
                {
                    if (!sellFull)
                    {
                        var sqlUpdateUser = @$"UPDATE public.deals set total_volume = total_volume - @volume
                                                                                WHERE deal_id = @deal_id";
                        var paramUpdateUser = new Dictionary<string, object>
                             {
                                    { $"@deal_id", dealId },
                                    { $"@volume", transactionsEntity.volume } ,
                             };
                        await _uow.ExecuteDefault(sqlUpdateUser, paramUpdateUser);
                    }
                    else
                    {
                        var sqlDel = @$"delete from public.deals WHERE deal_id = @deal_id";
                        var paramDel = new Dictionary<string, object>
                             {
                                    { $"@deal_id", dealId }
                             };
                        await _uow.ExecuteDefault(sqlDel, paramDel);
                    }
                }

                if (buys.Count > 0)
                {
                    decimal amounts = 0;
                    int volumes = 0;
                    int startVolume = transactionsEntity.volume;
                    for (int i = 0; i < buys.Count; i++)
                    {
                        var buy = buys[i];
                        if (startVolume >= buy.volume)
                        {
                            startVolume -= buy.volume;
                            buy.done = true;
                            buy.rest = 0;
                            amounts += transactionsEntity.order_price * buy.volume;
                            volumes += buy.volume;
                        }
                        else
                        {
                            buy.rest = buy.volume - startVolume;
                            amounts += transactionsEntity.order_price * startVolume;
                            volumes += startVolume;
                            startVolume = 0;
                        }
                    }
                    var stock = await this.GetByIdAsync(transactionsEntity.stock_id);

                    // Cập nhật các lệnh bán
                    foreach (var buy in buys)
                    {
                        // Nếu thực hiện bán hết thành công thực hiện cập nhật trạng thái  của lệnh bán về thành công
                        var paramTran = new Dictionary<string, object>
                             {
                                    { $"@transactions_id", buy.transactions_id}
                             };
                        if (buy.done)
                        {

                            string sqlDeleteTran = @$"update transactions set status = 1 where transactions_id = @transactions_id;";
                            await _uow.ExecuteDefault(sqlDeleteTran, paramTran);
                        }
                        // Nếu chỉ thực hiện mua được 1 phần thì tạo ra 1 lệnh mua thành công và cập nhật lại số lượng lệnh mua cũ 
                        else
                        {
                            if (stock != null && (buy.rest < buy.volume && buy.rest > 0))
                            {
                                var newTran = new TransactionsEntity
                                {
                                    transactions_id = Guid.NewGuid(),
                                    stock_id = buy.stock_id,
                                    user_id = buy.user_id,
                                    stock_code = stock.stock_code,
                                    transaction_type = 0,
                                    created_at = DateTime.Now,
                                    order_price = buy.matched_price,
                                    matched_price = stock.matched_price,
                                    volume = buy.volume - buy.rest,
                                    status = 1
                                };
                                await base.BaseInsertAsync(newTran, "transactions");
                            }

                            paramTran.Add("@volume", buy.rest);
                            string sqlUpdateTran = @$"update transactions set volume = @volume where transactions_id = @transactions_id;";
                            await _uow.ExecuteDefault(sqlUpdateTran, paramTran);
                        }
                        // Tạo deal nắm giữ với mỗi lệnh mua thành công
                        if (buy.rest > 0 && buy.rest < buy.volume && stock != null)
                        {
                            var newDeal = new DealEntity
                            {
                                deal_id = Guid.NewGuid(),
                                stock_id = buy.stock_id,
                                stock_code = stock.stock_code,
                                user_id = buy.user_id,
                                total_volume = buy.volume - buy.rest,
                                total_tradeable_volume = stock.tradable_volume,
                                matched_price = Math.Round(transactionsEntity.order_price, 2)
                            };
                            await base.BaseInsertAsync(newDeal, "deals");
                            await this.SendEmail(stock.stock_code, buy.user_id, buy.volume - buy.rest, "Mua");
                        }
                        // Sau khi bán thành công tiến hành cập nhật lại tài sản của người bán
                        // Cập nhật lại thay đổi số tiền của người dùng                        
                        var user1 = await this.UpdateAssetUserAsync(buy.user_id, buy.volume * buy.matched_price - ((buy.volume - buy.rest) * transactionsEntity.order_price + buy.rest * buy.matched_price), "+");
                        await this.InsertTableAssetHistoryAsync(user1);
                    }

                    // Tiến hành cập nhật lại các lệnh mua và tài sản của người mua
                    if (startVolume == 0)
                    { // Nếu người bán bán được hết khối lượng đặt => cập nhật trạng thái giao dịch về thành công
                        transactionsEntity.status = 1;
                        await base.BaseInsertAsync(transactionsEntity, "transactions");
                    }
                    else
                    {
                        // Nếu chỉ bán được 1 phần thì vẫn thêm giao dịch nhưng khối lượng đặt bằng khối lượng chưa bán được 
                        transactionsEntity.status = 0;
                        transactionsEntity.volume = transactionsEntity.volume - startVolume;
                        await base.BaseInsertAsync(transactionsEntity, "transactions");

                        if (stock != null)
                        {
                            var newTran = new TransactionsEntity
                            {
                                transactions_id = Guid.NewGuid(),
                                stock_id = transactionsEntity.stock_id,
                                user_id = transactionsEntity.user_id,
                                stock_code = stock.stock_code,
                                transaction_type = 1,
                                created_at = DateTime.Now,
                                order_price = transactionsEntity.order_price,
                                matched_price = stock.matched_price,
                                volume = startVolume,
                                status = 1
                            };
                            await base.BaseInsertAsync(newTran, "transactions");
                        }
                    }
                    // Cập nhật lại thay đổi số tiền của người dùng                    
                    var user = await this.UpdateAssetUserAsync(transactionsEntity.user_id, amounts / 1000, "+");
                    await this.InsertTableAssetHistoryAsync(user);
                }
                else
                {
                    // Nếu không có lệnh mua nào được khớp
                    transactionsEntity.status = 0;
                    // Thực hiện thêm mới giao dịch
                    await base.BaseInsertAsync(transactionsEntity, "transactions");

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Hàm cập nhật lại các deal cùng mã chứng khoán của từng người dùng
        /// </summary>
        /// <param name="stockId">Id của mã chứng khoán</param>
        /// <returns></returns>
        /// Created by: nttue (13/11/2024)
        private async Task UpdateDealAsync(Guid stockId)
        {
            var param = new Dictionary<string, object>
                             {
                                    { $"@p_stock_id", stockId }
                             };
            var sql = "select * from public.update_deal(@p_stock_id);";
            await _uow.Connection.ExecuteAsync(sql, param);
        }
        private async Task<UserEntity?> UpdateAssetUserAsync(Guid userId, decimal amount, string operation = "-")
        {
            var sqlUpdateUser = @$"update ""user"" u
                                                                            set cash_value = u.cash_value {operation} @amount,
	                                                                            total_net_assets  = u.total_net_assets {operation} @amount
                                                                            where user_id = @user_id returning *";
            var paramUpdateUser = new Dictionary<string, object>
                             {
                                    { $"@user_id", userId },
                                    { $"@amount", amount * 1000 }
                             };
            var user = await _uow.QueryDefault<UserEntity>(sqlUpdateUser, paramUpdateUser);

            return user;
        }
        private async Task InsertTableAssetHistoryAsync(UserEntity? user)
        {
            // Thực hiện thêm dữ liệu vào bảng thay đổi lịch sử tài khoản của người dùng
            if (user != null)
            {

                var tableAssetHistoryEntity = new TableAssetHistoryEntity
                {
                    table_asset_history_id = Guid.NewGuid(),
                    user_id = user.user_id,
                    cash_value = user.cash_value,
                    total_net_assets = user.total_net_assets,
                    stock_value = user.stock_value
                };

                await base.BaseInsertAsync(tableAssetHistoryEntity, "table_asset_history");
            }
        }
        private async Task<DealEntity?> GetTotalVolumeByUserId(Guid userId)
        {
            var param = new Dictionary<string, object>
             {
                    { $"@userId", userId }
             };

            var sql = @"select d.deal_id, sum(d.total_volume) as total_volume from deals d where d.user_id  = @userId group by d.deal_id;";
            var entity = await _uow.QueryDefault<DealEntity>(sql, param);
            return entity;
        }
        public async Task<List<TableAssetHistoryModel>> GetAssetHistoryByUserAsync(Guid userId, PeriodEnum periodEnum)
        {
            var paramDictionary = new Dictionary<string, object>()
                {
                    {"p_user_id", userId }
                };
            var param = new DynamicParameters(paramDictionary);

            // Xác định khoảng thời gian dựa trên periodEnum
            string interval = periodEnum switch
            {
                PeriodEnum.Week1 => "1 week",
                PeriodEnum.Month1 => "1 month",
                PeriodEnum.Month6 => "6 months",
                _ => "1 year" // Mặc định là 1 năm nếu không thuộc các trường hợp trên
            };

            // Câu truy vấn SQL tổng quát với khoảng thời gian động
            var sql = @$"
                    with v_temp as (
	                                            select 
		                                            user_id,
		                                            total_net_assets,
		                                            stock_value,
		                                            cash_value,
		                                            dense_rank() over (partition by user_id, DATE(created_at) order by created_at desc) as rnk,
		                                            created_at 
	                                            from table_asset_history
                                            )
                                              SELECT distinct 
                                                  *
                                              FROM v_temp 
                    WHERE user_id = @p_user_id and rnk = 1
                    AND  DATE(created_at) BETWEEN NOW() - INTERVAL '{interval}' AND NOW() order by created_at asc ;";

            var result = await _uow.Connection.QueryAsync<TableAssetHistoryModel>(sql, param, commandType: CommandType.Text);

            return result.ToList();
        }
        public override async Task<StockEntity?> GetByIdAsync(Guid id)
        {
            var param = new Dictionary<string, object>
                {
                    { $"@stock_id", id }
                };

            var sql = $"select * from public.stocks where stock_id = @stock_id";

            var entity = await _uow.QueryDefault<StockEntity>(sql, param);

            return entity;
        }
        public async Task<object?> GetAssetDashboard(Guid userId)
        {
            var param = new Dictionary<string, object>
                {
                    { $"@userId", userId }
                };

            var sql = @"select 
	                                            sum((s.matched_price - d.matched_price) * d.total_volume * 1000) 
	                                            over (partition by d.user_id) 	as profit,
	                                            u.total_net_assets	
                                            from deals d 
                                            inner join stocks s on s.stock_id = d.stock_id 
                                            inner join ""user"" u on u.user_id = d.user_id 
                                            where u.user_id = @userId;";
            var result = await _uow.QueryDefault<object>(sql, param);

            return result;
        }

        public async Task<List<string>> GetAllStockCodeAsync()
        {
            var result = new List<string>();
            string sql = @"select stock_code from stocks where modified_date <> date(now());";
            var stocks = await _uow.QueryDefault<StockEntity>(sql);
            if (stocks != null && stocks.Count > 0)
            {
                result = stocks.Select(stock => stock.stock_code).ToList();
            }

            return result;
        }
        public async Task UpdateStockAsync(string sql, object param)
        {
            await _uow.ExecuteDefault(sql, param);
        }

        public async Task<Dictionary<string, List<StockEntity>>> GetReportStockAsync()
        {
            var result = new Dictionary<string, List<StockEntity>>();
            var sql = @"
                                        SELECT * FROM stocks 
                                        WHERE change_price IS NOT NULL 
                                        ORDER BY change_price DESC 
                                        LIMIT 10;
        
                                        SELECT * FROM stocks 
                                        WHERE change_price IS NOT NULL 
                                        ORDER BY change_price ASC 
                                        LIMIT 10;

                                        SELECT * FROM stocks 
                                        WHERE change_price IS NOT NULL 
                                        ORDER BY total_assets DESC 
                                        LIMIT 10;";

            try
            {
                using (var multi = await _uow.Connection.QueryMultipleAsync(sql))
                {
                    var descendingStocks = multi.Read<StockEntity>().ToList();
                    var ascendingStocks = multi.Read<StockEntity>().ToList();
                    var voulumeStocks = multi.Read<StockEntity>().ToList();

                    result.Add("desc", descendingStocks);
                    result.Add("asc", ascendingStocks);
                    result.Add("volume", voulumeStocks);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to fetch stock report. Please try again later.", ex);
            }

            return result;
        }

        public async Task UpdateAssetUserAsync()
        {
            var sql = "select * from public.update_asset_user();";
            await _uow.Connection.ExecuteAsync(sql);
        }
        public async Task SendEmail(string stockCode, Guid userId, int volume, string action)
        {
            var user = await _userDL.GetByIdAsync(userId);
            if (user == null)
            {
                return;
            }
            var htmlBody = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
  <meta charset=""UTF-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <title>Stock Notification</title>
</head>
<body style=""margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #f4f4f4; color: #333;"">

  <table style=""width: 100%; max-width: 600px; margin: 0 auto; background-color: #fff; border-collapse: collapse; border: 1px solid #ddd;"">
    <tr>
      <td style=""background-color: #4caf50; color: #fff; text-align: center; padding: 20px;"">
        <h1 style=""margin: 0; font-size: 24px;"">Thông báo</h1>
      </td>
    </tr>
    <tr>
      <td style=""padding: 20px; text-align: center;"">
        <h2 style=""color: #4caf50; margin: 0;"">Thành công!</h2>
        <p style=""font-size: 16px; margin: 10px 0;"">
          {action} thành công <strong style=""color: #333;"">{volume}</strong> mã <strong style=""color: #333;"">{stockCode}</strong> trong tài khoản.
        </p>
        <p style=""font-size: 14px; color: #666; margin: 20px 0;"">
          Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi.
        </p>
        <a href=""http://150.95.113.231/purchase"" style=""display: inline-block; padding: 10px 20px; background-color: #4caf50; color: #fff; text-decoration: none; border-radius: 5px; font-size: 16px;"">
          Xem chi tiết
        </a>
      </td>
    </tr>
    <tr>
      <td style=""background-color: #f4f4f4; text-align: center; padding: 10px;"">
        <p style=""font-size: 12px; color: #888; margin: 0;"">
          © 2024 Copyright. Tất cả các quyền được bảo lưu.
        </p>
      </td>
    </tr>
  </table>

</body>
</html>
";
            var request = new EmailDto()
            {
                Subject = "Thông báo",
                To = user.email,
                Body = $"Thông báo {action} mã {stockCode} thành công!",
                HTMLBody = htmlBody
            };
            _email.SendMail(request);
        }

    }
}
