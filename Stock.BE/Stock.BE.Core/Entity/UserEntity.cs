using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.BE.Core.Entity
{
    /// <summary>
    /// Bảng chứa thông tin người dùng
    /// </summary>
    [Table("user")]
    public class UserEntity
    {
        [Key]
        public Guid user_id { get; set; }
        /// <summary>
        /// Tên đăng nhập của người dùng
        /// </summary>
        public string user_name { get; set; } = string.Empty;
        /// <summary>
        /// Email người dùng
        /// </summary>
        public string email { get; set; } = string.Empty;
        /// <summary>
        /// PasswordHash là một mảng byte chứa giá trị băm (hash) của mật khẩu người dùng.
        /// </summary>
        public string password { get; set; }
        public double total_net_assets { get; set; } = 0;
        public double stock_value { get; set; } = 0;
        public double cash_value { get; set; } = 0;

    }
}
