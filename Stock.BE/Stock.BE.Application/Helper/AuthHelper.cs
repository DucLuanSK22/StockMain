using System.Security.Cryptography;
using System.Text;

namespace ESP.Cloud.BE.Application.Helper
{
    public class AuthHelper
    {
        public static void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            try
            {
                using var hmac = new HMACSHA512();
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool VetifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }

        public static string GenerateRandomPassword()
        {
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digitChars = "0123456789";
            const string specialChars = "&$!%@";

            Random random = new Random();
            StringBuilder password = new StringBuilder();

            // Đảm bảo chứa ít nhất một ký tự đặc biệt, một chữ thường, một chữ hoa, một số và tối thiểu 8 ký tự
            password.Append(specialChars[random.Next(specialChars.Length)]);
            password.Append(lowerChars[random.Next(lowerChars.Length)]);
            password.Append(upperChars[random.Next(upperChars.Length)]);
            password.Append(digitChars[random.Next(digitChars.Length)]);

            // Tạo phần còn lại của mật khẩu (tối thiểu 4 ký tự)
            for (int i = 4; i < 8; i++)
            {
                string allChars = lowerChars + upperChars + digitChars + specialChars;
                int randomIndex = random.Next(0, allChars.Length);
                password.Append(allChars[randomIndex]);
            }

            return password.ToString();
        }
    }
}
