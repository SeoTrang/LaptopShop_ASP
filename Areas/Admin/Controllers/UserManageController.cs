using System;
using System.Collections.Generic;
// using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LapTopShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LapTopShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManageController : Controller
    {
        private readonly dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<UserManageController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManageController(
            ILogger<UserManageController> logger,
            dbContext context,
            IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager // Inject UserManager<ApplicationUser>
        )
        {
            _logger = logger;
            _userManager = userManager;
            _env = env;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Lấy tất cả người dùng
            var users = await _userManager.Users
                        .Include(u => u.Orders)
                        .ToListAsync();
            foreach (var user in users)
            {
                // Lấy các vai trò của người dùng
                var roles = await _userManager.GetRolesAsync(user);
                
                // Gán danh sách vai trò vào thuộc tính Roles của ApplicationUser
                user.Roles = roles.ToList();

            }
            // Truyền danh sách người dùng vào view để hiển thị
            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}