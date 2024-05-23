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

        public ICollection<Order> Orders { get; set; }
        
         [NotMapped]
        public List<string> Roles { get; set; } = new List<string>(); // Add this property
        
        
    }
}