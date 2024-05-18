using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LapTopShop.Models;
using LapTopShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LapTopShop.Controllers
{
    public class ProfileController : Controller
    {
        private readonly dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ProfileController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(
            dbContext context,
            IWebHostEnvironment env,
            ILogger<ProfileController> logger,
            UserManager<ApplicationUser> userManager) // Inject UserManager<ApplicationUser>
        {
            _context = context;
            _env = env;
            _logger = logger;
            _userManager = userManager; // Khởi tạo UserManager
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(this.User);
            if(user == null){
                return Redirect("/login");
            }

            var orders = await _context.Orders
            .Where(o => o.UserId == user.Id)
            .ToListAsync();
            System.Console.WriteLine("hello ------------------------------------------------------");
            System.Console.WriteLine(orders);
            ProfileViewModel profileViewModel = new ProfileViewModel{
                OrderList = orders
            };
            return View(profileViewModel);
        }
    }
}