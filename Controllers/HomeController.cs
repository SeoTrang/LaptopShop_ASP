using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using LapTopShop.Models;
using Microsoft.EntityFrameworkCore;
using LapTopShop.ViewModels;

namespace LapTopShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly dbContext _context;

    public HomeController(ILogger<HomeController> logger, dbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var saleHot = await _context.Products
                .Include(p => p.Category) // Nạp thông tin Category
                .Take(5) // Giới hạn số lượng bản ghi
                .ToListAsync(); // Lấy danh sách 5 bản ghi

            var office_laptop = await _context.Products
                .Where(p => p.TypeLaptop == "Văn Phòng") // Lọc sản phẩm theo TypeLaptop
                .Include(p => p.Category) // Nạp thông tin Category
                .Take(10) // Giới hạn số lượng bản ghi
                .ToListAsync(); // Lấy danh sách sản phẩm Văn Phòng
            
            var gaming_laptop = await _context.Products
                .Where(p => p.TypeLaptop == "Gaming") // Lọc sản phẩm theo TypeLaptop
                .Include(p => p.Category) // Nạp thông tin Category
                .Take(10) // Giới hạn số lượng bản ghi
                .ToListAsync(); // Lấy danh sách sản phẩm Văn Phòng

            var Graphics_Laptop = await _context.Products
                .Where(p => p.TypeLaptop == "Đồ Họa") // Lọc sản phẩm theo TypeLaptop
                .Include(p => p.Category) // Nạp thông tin Category
                .Take(10) // Giới hạn số lượng bản ghi
                .ToListAsync(); // Lấy danh sách sản phẩm Văn Phòng

            var Laptop_for_you = await _context.Products
                .Include(p => p.Category) // Nạp thông tin Category
                .Take(10) // Giới hạn số lượng bản ghi
                .ToListAsync(); // Lấy danh sách sản phẩm Văn Phòng
            

            HomeViewModel homeViewModel = new HomeViewModel
            {
                SaleHotProducts = saleHot,
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
        
    }

    // public IActionResult 
}
