using System.Data.Common;

namespace Stock.BE.Core.DL
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        #region Properties
        DbConnection Connection { get; }
        DbTransaction? Transaction { get; }
        #endregion

        #region Methods
        /// <summary>
        ///  Hàm mở transaction đồng bộ
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        void BeginTransaction();
        /// <summary>
        ///  Hàm mở transaction bất đồng bộ
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        Task BeginTransactionAsync();

        /// <summary>
        ///  Hàm commit transaction đồng bộ
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        void Commit();

        /// <summary>
        /// Hàm commit transaction bất đồng bộ
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        Task CommitAsync();
        /// <summary>
        /// Hàm rollback xử lý đồng bộ transaction
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)

        void Rollback();
        /// <summary>
        /// Hàm rollback xử lý bất đồng bộ transaction
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        Task RollbackAsync();
        /// <summary>
        /// Hàm query dữ liệu trong database lấy nhiều bản ghi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        Task<List<T>> QueryDefault<T>(string sql);
        Task<List<T>> QueryList<T>(string sql, object? param = null);
        /// <summary>
        /// Hàm query dữ liệu trong database lấy 1 bản ghi, có param
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        Task<T?> QueryDefault<T>(string sql, object? param = null);
        /// <summary>
        /// Hàm execute query trong database 
        /// Phục vụ cho việc thêm, sửa, xóa dữ liệu
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="sql">Câu truy vấn thêm, sửa, xóa</param>
        /// <param name="param">param</param>
        /// <returns></returns>
        /// Created by: nttue(14/02/2024)
        Task ExecuteDefault(string sql, object? param = null);

        #endregion

    }
}
