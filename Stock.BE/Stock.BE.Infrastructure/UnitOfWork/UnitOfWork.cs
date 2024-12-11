using System.Data.Common;
using System.Data;
using Npgsql;
using Dapper;
using Stock.BE.Core.DL;

namespace Stock.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private readonly DbConnection _connection;

        private DbTransaction? _transaction = null;
        #endregion

        #region Constructor
        public UnitOfWork(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
        }
        #endregion

        #region Methods
        public DbConnection Connection => _connection;
        public DbTransaction? Transaction => _transaction;

        /// <summary>
        ///  Hàm mở transaction đồng bộ
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        public void BeginTransaction()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.OpenAsync();
            }
            _transaction = _connection.BeginTransaction();

        }

        /// <summary>
        ///  Hàm mở transaction bất đồng bộ
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            if (_connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }

            _transaction = await _connection.BeginTransactionAsync();
        }


        /// <summary>
        ///  Hàm commit transaction đồng bộ
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        public void Commit()
        {
            _transaction?.Commit();
            Dispose();
        }

        /// <summary>
        /// Hàm commit transaction bất đồng bộ
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }

            await DisposeAsync();
        }

        /// <summary>
        /// Hàm đồng bộ giải phóng kết nối
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;
            _connection.Close();
        }

        /// <summary>
        /// Hàm bất đồng bộ giải phóng kết nối
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
            _transaction = null;
        }

        /// <summary>
        /// Hàm rollback xử lý đồng bộ transaction
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        public void Rollback()
        {
            _transaction?.Rollback();
            Dispose();
        }

        /// <summary>
        /// Hàm rollback xử lý bất đồng bộ transaction
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();

            }
            await DisposeAsync();
        }

        public async Task<List<T>> QueryDefault<T>(string sql)
        {
            var listEntity = await Connection.QueryAsync<T>(sql, transaction: Transaction);

            return (List<T>)listEntity;
        }
        public async Task<List<T>> QueryList<T>(string sql, object? param = null)
        {
            var parameters = new DynamicParameters(param);
            var listEntity = await Connection.QueryAsync<T>(sql, parameters, transaction: Transaction);
            return (List<T>)listEntity;
        }
        public async Task<T?> QueryDefault<T>(string sql, object? param = null)
        {
            var parameters = new DynamicParameters(param);
            var entity = await Connection.QueryFirstOrDefaultAsync<T>(sql, parameters, transaction: Transaction);
            return entity;
        }

        public async Task ExecuteDefault(string sql, object? param = null)
        {
            var parameters = new DynamicParameters(param);
            await Connection.ExecuteAsync(sql, parameters, transaction: Transaction);
        }


        #endregion
    }

}
