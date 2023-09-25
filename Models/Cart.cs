using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LapTopShop.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        // Thêm trường UserId để lưu Id của người dùng
        public string UserId { get; set; }

        // Sử dụng trường UserId làm trường khóa ngoại đến Id trong bảng ApplicationUser
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string Code { set; get; }
        public List<CartItem> CartItems { get; set; }
    }
}