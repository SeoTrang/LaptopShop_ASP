
using LapTopShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LapTopShop.Areas.Admin.Role
{

    public class RolePageModel: PageModel{
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly dbContext _context;

        [TempData]
        public string StatusMessage { get; set; }
        public RolePageModel(
             RoleManager<IdentityRole> _roleManager,
             dbContext _context
        )
        {
            this._roleManager = _roleManager;
            this._context = _context;
        }
    }
}