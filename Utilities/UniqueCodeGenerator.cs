using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LapTopShop.Utilities
{
   public static class UniqueCodeGenerator
    {
        public static string GenerateUniqueCode(int productId, int userId)
        {
            // Lấy id của sản phẩm và người dùng
            string productPart = productId.ToString();
            string userPart = userId.ToString();

            // Tạo một chuỗi ngẫu nhiên có độ dài 3-4 ký tự
            string randomPart = GenerateRandomString(3, 4);

            // Kết hợp tất cả các phần để tạo mã code
            string uniqueCode = $"{productPart}{randomPart}{userPart}";

            return uniqueCode;
        }

        public static string GenerateRandomString(int minLength, int maxLength)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = random.Next(minLength, maxLength + 1);
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}