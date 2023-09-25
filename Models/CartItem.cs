using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LapTopShop.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        
        // Sử dụng trường CartId làm trường khóa ngoại đến Id trong bảng Cart
        public Cart Cart { get; set; }
        [ForeignKey("Product")]

        public int ProductId { get; set; }

        public int Count { get; set; }
        
        // Sử dụng trường ProductId làm trường khóa ngoại đến Id trong bảng Product
        public Product Product { get; set; }
    }
}
