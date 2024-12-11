using Stock.BE.Core.BaseDL.Repository;
using Stock.BE.Core.DTO;
using Stock.BE.Core.Entity;

namespace Stock.BE.Core.DL
{
    public interface IUserDL : IBaseRepository<UserEntity>
    {
        /// <summary>
        /// Hàm lấy ra tài khoản người dùng theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// Created by: nttue - 20/11/2023
        Task<UserEntity> GetUserByEmailAsync(string email);

        Task UpdatePassWordAsync(byte[] passwordHash, byte[] passwordSalt, Guid user_id);

        Task InsertUser(UserDto userDto);
        
    }
}
