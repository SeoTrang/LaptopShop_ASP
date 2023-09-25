using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LapTopShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        // Thêm trường UserId để lưu Id của người dùng
        public string UserId { get; set; }

        // Sử dụng trường UserId làm trường khóa ngoại đến Id trong bảng ApplicationUser
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string Code { set; get; }

        [Required(ErrorMessage = "Phải nhập tên người nhận")]
        public string Receiver { set; get; }
        public string State { set; get; }
        public DateTime Date { set; get; }

        [Required(ErrorMessage = "Phải nhập địa chỉ")]
        public string Address { set; get; }

        [Required(ErrorMessage = "Phải nhập số điện thoại")]
        public string Phone { set; get; }
        public string? Email { set; get; }
        public string? Note { set; get; }
        public int TotalPrice { set; get; }
        public int? Discount { set; get; }
        
        
        public List<OrderItem> OrderItems { set; get; }

    }
}