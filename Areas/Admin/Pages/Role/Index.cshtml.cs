using System.Collections.Generic; // Import phần không gian tên
using System.Threading.Tasks; // Import phần không gian tên cho Task
// using System.Data.Entity;
using LapTopShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LapTopShop.Areas.Admin.Role
{
    public class IndexModel : RolePageModel
    {

        
        public IndexModel(RoleManager<IdentityRole> _roleManager, dbContext _context) : base(_roleManager, _context)
        {
        }

        public List<IdentityRole> Roles { get; set; }   

        public async Task OnGet() // Đổi tên phương thức thành OnGetAsync
        {
            Roles = await _roleManager.Roles.OrderBy(r=> r.Name).ToListAsync();
        }


        public void OnPost() => RedirectToPage();
    }
}
