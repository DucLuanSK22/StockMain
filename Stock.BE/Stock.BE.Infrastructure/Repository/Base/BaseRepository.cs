using Dapper;
using Stock.BE.Core.BaseDL.Repository;
using Stock.BE.Core.DL;

namespace Stock.BE.Infrastructure.Repository.Base
{
    public class BaseRepository<TEntity> : BaseReadRepository<TEntity>, IBaseRepository<TEntity>
    {
        #region Constructor
        public BaseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion
        public virtual async Task DeleteAsync(Guid id)
        {
            var param = new Dictionary<string, object>
                {
                    { $"@{TableName}_id", id }
                };

            var sql = $"delete from public.{TableName} where {TableName}_id = @{TableName}_id";

            await _uow.ExecuteDefault(sql, param);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entityProperties = typeof(TEntity).GetProperties();
            var param = new Dictionary<string, object>();
            var listParamNames = new List<string>();
            var listPropertyNames = new List<string>();

            foreach (var property in entityProperties)
            {
                var propertyName = property.Name;
                var paramValue = property.GetValue(entity);
                if (paramValue != null)
                {
                    param.Add("@" + propertyName, paramValue);
                    listParamNames.Add("@" + propertyName);
                    listPropertyNames.Add(propertyName);
                }
            }

            var sql = $"insert into public.{TableName}({string.Join(",", listPropertyNames)}) " +
                      $"values ({string.Join(",", listParamNames)})";

            await _uow.ExecuteDefault(sql, param);

            return entity;
        }

        public async Task InsertBatchAsync(List<TEntity> entity)
        {
            var entityProperties = typeof(TEntity).GetProperties();
            var param = new Dictionary<string, object>();
            var listParamNames = new List<string>();
            var listPropertyNames = new List<string>();

            foreach (var property in entityProperties)
            {
                var propertyName = property.Name;
                var paramValue = property.GetValue(entity[0]);
                if (paramValue != null)
                {
                    param.Add("@" + propertyName, paramValue);
                    listParamNames.Add("@" + propertyName);
                    listPropertyNames.Add(propertyName);
                }
            }

            var sql = $"insert into public.{TableName}({string.Join(",", listPropertyNames)}) " +
                      $"values ({string.Join(",", listParamNames)})";

            await _uow.Connection.ExecuteAsync(sql, entity);
        }

        public async Task<TEntity> UpdateAsync(Guid id, TEntity entity)
        {
            var entityProperties = typeof(TEntity).GetProperties();
            var param = new Dictionary<string, object>();
            var listParamNames = new List<string>();
            var listPropertyNames = new List<string>();

            foreach (var property in entityProperties)
            {
                var propertyName = property.Name;
                var paramValue = property.GetValue(entity);
                if (paramValue != null)
                {
                    param.Add("@" + propertyName, paramValue);
                    listParamNames.Add($"{propertyName} = @{propertyName}");
                    listPropertyNames.Add(propertyName);
                }
            }

            var sql = $"update public.{TableName} set {string.Join(", ", listParamNames)} " +
                      $"where {TableName}_id = @Id";

            param.Add("@Id", id);

            await _uow.ExecuteDefault(sql, param);

            return entity;
        }

        public async Task BaseInsertAsync<TModel>(TModel entity, string tableName)
        {
            var entityProperties = typeof(TModel).GetProperties();
            var param = new Dictionary<string, object>();
            var listParamNames = new List<string>();
            var listPropertyNames = new List<string>();

            foreach (var property in entityProperties)
            {
                var propertyName = property.Name;
                var paramValue = property.GetValue(entity);
                if (paramValue != null)
                {
                    param.Add("@" + propertyName, paramValue);
                    listParamNames.Add("@" + propertyName);
                    listPropertyNames.Add(propertyName);
                }
            }

            var sql = $"insert into public.{tableName}({string.Join(",", listPropertyNames)}) " +
                      $"values ({string.Join(",", listParamNames)})";

            await _uow.ExecuteDefault(sql, param);
        }
    }
}
