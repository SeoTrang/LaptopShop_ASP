using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LapTopShop.Models;
using LapTopShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LapTopShop.Controllers
{
    // [Route("[controller]")]
    public class DetailController : Controller
    {
        private readonly ILogger<DetailController> _logger;
        private readonly dbContext _context;

        public DetailController(ILogger<DetailController> logger, dbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task <IActionResult> Index(int id)
        {
            // _logger.LogInformation("gia tri id :"+id);
            // return Content("gia tri : " + id);
            try
            {
                // var item = _dbContext.Products
                // return View();
                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }
                

                var item = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.ListFileDetail) // Kết hợp thông tin từ bảng Img
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (item == null)
                {
                    return NotFound();
                }

                DetailViewModel detailViewModel = new DetailViewModel{
                    product = item
                };


                return View(detailViewModel);
                // return Content("Name : "+ detailViewModel.product.ProductName+ " - Img " + detailViewModel.product.ListFileDetail[0].FilePath);
            }
            catch (System.Exception)
            {
                
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