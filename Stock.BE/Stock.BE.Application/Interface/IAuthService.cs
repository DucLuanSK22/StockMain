using Stock.BE.Application.Dto;
using Stock.BE.Application.Param;

namespace Stock.BE.Application.Interface
{
    public interface IAuthService
    {
        /// <summary>
        /// Hàm đăng ký
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// Created by: nttue 15/02/2024
        Task<UserDto> Register(UserRegisterParam userDto);
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// Created by: nttue 15/02/2024
        Task<UserDto> Login(UserLoginParam userLoginDto);        
        /// <summary>
        /// Hàm lấy mật khẩu mới cho nguời dùng qua email
        /// </summary>
        /// <param name="email">email của người dùng</param>
        /// <returns></returns>
        /// Created by: nttue 15/02/2024
        Task GetNewPasswordAsync(string email);
    }
}
