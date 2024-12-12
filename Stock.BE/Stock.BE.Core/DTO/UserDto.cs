using System.ComponentModel.DataAnnotations;

namespace Stock.BE.Core.DTO;

public class UserDto
{
    /// <summary>
    /// Email người dùng
    /// </summary>
    public string email { get; set; } = string.Empty;

    public string password { get; set; } = string.Empty;

    /// <summary>
    /// Tên đăng nhập của người dùng
    /// </summary>
    public string user_name { get; set; } = string.Empty;
}
