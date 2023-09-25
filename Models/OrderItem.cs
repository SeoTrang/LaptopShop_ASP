using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LapTopShop.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        
        // Sử dụng trường CartId làm trường khóa ngoại đến Id trong bảng Cart
        public Order Order { get; set; }
        
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public int Count { get; set; }
        
        // Sử dụng trường ProductId làm trường khóa ngoại đến Id trong bảng Product
        // public Product Product { get; set; }
        public string ProductName { get; set; }
        public string Avatar { get; set; }
        public int Price { get; set; }
        public string Status { set; get; }
    }
}