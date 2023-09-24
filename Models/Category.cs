using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LapTopShop.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }

        // [BindProperty]
        [Required(ErrorMessage = "Phải Nhập Tên")]
        [Column(TypeName = "Nvarchar")]
        [StringLength(50)]
        [Display(Name ="Tên Danh Mục")]
        
        public string Name { get; set; }

        // [BindProperty]
        public string Avatar { get; set; }

        [BindProperty]
        [DataType(DataType.Upload)]
        [Required(ErrorMessage ="Chọn file upload")]
        [NotMapped]
        public IFormFile MainImageFile { get; set; }
        public List<Product> Products { get; set; }
    }
}