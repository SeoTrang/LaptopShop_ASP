using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LapTopShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LapTopShop.Areas.Admin.Controllers
{
    // [Route("[controller]")]
    [Area("Admin")]
    public class OrderManageController : Controller
    {
        private readonly dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<OrderManageController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderManageController(
            dbContext context,
            IWebHostEnvironment env,
            ILogger<OrderManageController> logger,
            UserManager<ApplicationUser> userManager) // Inject UserManager<ApplicationUser>
        {
            _context = context;
            _env = env;
            _logger = logger;
            _userManager = userManager; // Khởi tạo UserManager
        }

        public IActionResult Index()
        {
            try
            {
                var ordersWithItems = _context.Orders
                    .Include(o => o.OrderItems) // Kèm các OrderItems liên quan
                    .ToList();

                _logger.LogInformation("oder id : " + ordersWithItems[0].Id);
                if(ordersWithItems[0].OrderItems != null ){
                    _logger.LogInformation("hello");

                }else{
                    _logger.LogInformation("hello2");
                }
                if (ordersWithItems != null && ordersWithItems[0] != null && ordersWithItems[0].OrderItems != null && ordersWithItems[0].OrderItems[0] != null)
                {
                    _logger.LogInformation("oderitem id : " + ordersWithItems[0].OrderItems[0].Id);
                }
                ViewBag.Orders = ordersWithItems;
                return View();
            }
            catch (System.Exception)
            {
                // Xử lý ngoại lệ
                throw;
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}