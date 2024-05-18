using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LapTopShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }

       // Không nên lưu mật khẩu trực tiếp dưới dạng plain text
    // [Column(TypeName ="nvarchar(100)")]
    // public string Password { get; set; }
        
    }
}