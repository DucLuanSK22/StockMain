namespace Stock.BE.Core.DTO
{
    public class UserLoginParam
    {
        /// <summary>
        /// Email người dùng
        /// </summary>
        public string email { get; set; } = string.Empty;
        /// <summary>
        /// PasswordHash là một mảng byte chứa giá trị băm (hash) của mật khẩu người dùng.
        /// </summary>
        public string password { get; set; }
    }
}
