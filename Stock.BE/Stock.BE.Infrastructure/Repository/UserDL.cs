using Dapper;
using Stock.BE.Core;
using Stock.BE.Core.DL;
using Stock.BE.Core.DTO;
using Stock.BE.Core.Entity;
using Stock.BE.Core.Model;
using Stock.BE.Infrastructure.Repository.Base;

namespace Stock.BE.Infrastructure.Repository;
public class UserDL : BaseRepository<UserEntity>, IUserDL
{
    public UserDL(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<UserEntity> GetUserByEmailAsync(string email)
    {
        try
        {
            var param = new Dictionary<string, object>
                {
                    { $"@email", email }
                };
            var sql = @"select * from ""user"" d where email = @email;";
            var result = await _uow.QueryDefault<UserEntity>(sql, param);
            return result;
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public Task UpdatePassWordAsync(byte[] passwordHash, byte[] passwordSalt, Guid user_id)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// Hàm thêm mới 1 user vào cơ sở dữ liệu
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns></returns>
    public async Task InsertUser(UserDto userDto)
    {
        var user = new UserEntity
        {
            user_id = Guid.NewGuid(),
            email = userDto.email,
            user_name = userDto.user_name,
            password = userDto.password,
            total_net_assets = 0,
            stock_value = 0,
            cash_value = 0
        };
        await base.BaseInsertAsync(user, "user");
    }

}