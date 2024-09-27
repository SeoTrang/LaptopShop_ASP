using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LapTopShop.Models;
using LapTopShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LapTopShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbContext _context;

        public ProductController(ILogger<HomeController> logger, dbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string status)
        {

            try
            {
                // var saleHot = await _context.Products
                //     .Include(p => p.Category) // Nạp thông tin Category
                //     .Take(5) // Giới hạn số lượng bản ghi
                //     .ToListAsync(); // Lấy danh sách 5 bản ghi

                var office_laptop = await _context.Products
                    .Where(p => p.TypeLaptop == "Văn Phòng" && p.Status == status) // Lọc sản phẩm theo TypeLaptop và Status
                    .Include(p => p.Category) // Nạp thông tin Category
                    .Take(10) // Giới hạn số lượng bản ghi
                    .ToListAsync(); // Lấy danh sách sản phẩm Văn Phòng

                var gaming_laptop = await _context.Products
                    .Where(p => p.TypeLaptop == "Gaming" && p.Status == status) // Lọc sản phẩm theo TypeLaptop và Status
                    .Include(p => p.Category) // Nạp thông tin Category
                    .Take(10) // Giới hạn số lượng bản ghi
                    .ToListAsync(); // Lấy danh sách sản phẩm Gaming

                var Graphics_Laptop = await _context.Products
                    .Where(p => p.TypeLaptop == "Đồ Họa" && p.Status == status) // Lọc sản phẩm theo TypeLaptop và Status
                    .Include(p => p.Category) // Nạp thông tin Category
                    .Take(10) // Giới hạn số lượng bản ghi
                    .ToListAsync(); // Lấy danh sách sản phẩm Đồ Họa

                var Laptop_for_you = await _context.Products
                    .Where(p => p.Status == status) // Lọc sản phẩm theo Status
                    .Include(p => p.Category) // Nạp thông tin Category
                    .Take(10) // Giới hạn số lượng bản ghi
                    .ToListAsync(); // Lấy danh sách sản phẩm Mới

                HomeViewModel homeViewModel = new HomeViewModel
                {
                    OfficeLaptops = office_laptop,
                    GamingLaptops = gaming_laptop,
                    GraphicsLaptops = Graphics_Laptop,
                    LaptopForYou = Laptop_for_you
                };
                return View(homeViewModel);

            }
            catch (System.Exception)
            {
                
                throw;
            }
            // return View();
            // return Content("status : " + status);
        }

        public async Task<IActionResult> Search(string search)
        {
            try
            {
                var searchResults = await _context.Products
                    .Where(p => EF.Functions.Like(p.ProductName, $"%{search}%")) // Tìm kiếm gần đúng theo tên sản phẩm
                    .Include(p => p.Category) // Nạp thông tin Category
                    .ToListAsync(); // Lấy danh sách sản phẩm

                
                ViewBag.Products = searchResults;
                return View(); // Trả về view Index với kết quả tìm kiếm
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for products.");
                return View("Error");
            }
        }
    }

    

}