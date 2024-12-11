using Stock.BE.Core.BaseDL.Repository;
using Stock.BE.Core.DL;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Stock.BE.Infrastructure.Repository
{
    public class BaseReadRepository<TEntity> : IBaseReadRepository<TEntity>
    {
        #region Fields
        protected readonly IUnitOfWork _uow;
        /// <summary>
        /// Sử dụng protected cho set để class con có thể sửa lại được TableName
        /// </summary>
        public virtual string TableName { get; protected set; } = typeof(TEntity).GetCustomAttribute<TableAttribute>()?.Name;
        #endregion

        #region Constructor
        public BaseReadRepository(IUnitOfWork entityOfWork)
        {
            _uow = entityOfWork;
        }
        #endregion

        /// <summary>
        /// Hàm lấy cả bản ghi của một bảng
        /// </summary>
        /// <returns>Trả về tất cả các bản ghi</returns>
        /// Created by: nttue(14/02/2024) 
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            var sql = $"select * from public.{TableName}";

            var listEntity = await _uow.QueryDefault<TEntity>(sql);

            return listEntity;
        }

        /// <summary>
        /// Hàm lấy một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>bản ghi</returns>
        /// Created by: nttue(14/02/2024)
        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var param = new Dictionary<string, object>
                {
                    { $"@{TableName}_id", id }
                };

            var sql = $"select * from public.{TableName} where {TableName}_id = @{TableName}_id";

            var entity = await _uow.QueryDefault<TEntity>(sql, param);

            return entity;
        }
    }
}
