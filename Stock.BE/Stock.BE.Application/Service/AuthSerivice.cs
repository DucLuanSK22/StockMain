using AutoMapper;
using Stock.BE.Application.Dto;
using ESP.Cloud.BE.Application.Helper;
using Stock.BE.Application.Param;
using Stock.BE.Application.Interface;
using Microsoft.Extensions.Configuration;
using Stock.BE.Core.ESPException;
using Stock.BE.Core.Resource;
using Stock.BE.Core.Model;
using Stock.BE.Core.DL.UserDL;

namespace Stock.BE.Application.Service
{
    public class AuthSerivice : IAuthService
    {
        private readonly IUserDL _userDL;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthSerivice(IUserDL userDL, IMapper mapper, IConfiguration config)
        {
            _userDL = userDL;
            _mapper = mapper;
            _config = config;
        }

        public async Task GetNewPasswordAsync(string emailReset)
        {
            var userExists = await _userDL.GetUserByEmailAsync(emailReset);
            if (userExists == null)
            {
                throw new ConflictException(Resource.UserNotExists);
            }
            var password = AuthHelper.GenerateRandomPassword();

            AuthHelper.CreatePassword(password, out byte[] passwordHash, out byte[] passwordSalt);

            await _userDL.UpdatePassWordAsync(passwordHash, passwordSalt, userExists.user_id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        /// <exception cref="ConflictException"></exception>
        /// <exception cref="AuthException"></exception>
        public async Task<UserDto> Login(UserLoginParam userLoginDto)
        {
            var userExists = await _userDL.GetUserByEmailAsync(userLoginDto.email);
            if (userExists == null)
            {
                throw new ConflictException(Resource.UserNotExists);
            }
            if (!AuthHelper.VetifyPassword(userLoginDto.password, userExists.password_hash, userExists.password_salt))
            {
                throw new AuthException(Resource.IncorrectPassword);
            }

            var result = _mapper.Map<UserDto>(userExists);

            return result;
        }

        public async Task<UserDto> Register(UserRegisterParam userDto)
        {
            try
            {
                var userExists = await _userDL.GetUserByEmailAsync(userDto.email);
                if (userExists != null)
                {
                    throw new ConflictException(Resource.UserExists);
                }
                AuthHelper.CreatePassword(userDto.password, out byte[] passwordHash, out byte[] passwordSalt);
                var user = _mapper.Map<UserEntity>(userDto);

                user.password_hash = passwordHash;
                user.password_salt = passwordSalt;

                user.user_id = Guid.NewGuid();

                await _userDL.InsertAsync(user);

                var result = _mapper.Map<UserDto>(user);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
