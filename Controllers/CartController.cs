using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LapTopShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Extensions;
using LapTopShop.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LapTopShop.Controllers
{
    // [Route("[controller]")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<CartController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(
            dbContext context,
            IWebHostEnvironment env,
            ILogger<CartController> logger,
            UserManager<ApplicationUser> userManager) // Inject UserManager<ApplicationUser>
        {
            _context = context;
            _env = env;
            _logger = logger;
            _userManager = userManager; // Khởi tạo UserManager
        }

        public async Task<IActionResult> Index()
        {
            // var user = await _userManager.GetUserAsync(this.User) ?? null;
            // // Sử dụng Include để nạp cả CartItems và Product liên quan đến mỗi CartItem
            // var carts = await _context.Carts
            //     .Include(c => c.CartItems)
            //     .ThenInclude(ci => ci.Product)
            //     .ToListAsync();

            var user = await _userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return Redirect("/login");
            }

            var carts = await _context.Carts
                .Where(c => c.UserId == user.Id)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync();

            if (carts == null)
            {
                // Nếu không tìm thấy giỏ hàng, bạn có thể thực hiện xử lý tương ứng ở đây
                // Ví dụ: trả về một trang thông báo không có giỏ hàng
                return View();
            }

            // Ghi log và trả về view
            _logger.LogInformation($"cart id : {carts.Id} ");
            foreach (var item in carts.CartItems)
            {
                _logger.LogInformation($"CartItem ID: {item.Id}, Product ID: {item.ProductId}, Count: {item.Count}");
            }
            ViewBag.Cart = carts;
            return View();

            // return View(carts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


        [HttpPost]
        public async Task<ActionResult> AddCart(int ProductId, int Count, string url){
            // var url = HttpContext.Request.GetDisplayUrl();
            var user = await _userManager.GetUserAsync(this.User) ?? null;
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"ProductId: {ProductId}");
            _logger.LogInformation($"Count: {Count}");
            if (user == null)
            {
                return Redirect("/login");
            }
            try
            {  

                var existingCartItem = await _context.CartItems
                    .FirstOrDefaultAsync(ci => ci.ProductId == ProductId && ci.Cart.UserId == user.Id);
                var existingCart = await _context.Carts
                    .FirstOrDefaultAsync(c => c.UserId == user.Id);

                if (existingCartItem != null)
                {
                    //  đã tồn tại trong giỏ hàng
                    // return Content("Sản phẩm đã tồn tại trong giỏ hàng");
                    // Truyền giá trị vào TempData
                    TempData["Message"] = "Sản phẩm đã tồn tại trong giỏ hàng";
                    // Sau đó thực hiện redirect
                    return Redirect(url);
                }
                int cartId = -1;

                if (existingCart != null)
                {
                    // user đã có giỏ hàng
                    cartId = existingCart.Id;
                    CartItem cartItem = new CartItem();
                    cartItem.Count = Count;
                    cartItem.ProductId = ProductId;
                    cartItem.CartId = cartId;

                    await _context.CartItems.AddAsync(cartItem);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Cart cart = new Cart();
                    cart.Code = "#" + UniqueCodeGenerator.GenerateUniqueCode(2, 5);
                    cart.UserId = user.Id;
                    await _context.Carts.AddAsync(cart);
                    await _context.SaveChangesAsync();
                    cartId = cart.Id;

                    CartItem cartItem = new CartItem();
                    cartItem.Count = Count;
                    cartItem.ProductId = ProductId;
                    cartItem.CartId = cartId;

                    await _context.CartItems.AddAsync(cartItem);
                    await _context.SaveChangesAsync();
                }

                


                


                // _logger.LogInformation($"Code: {cart.Code}");
                // _logger.LogInformation($"userId:{user.Id}");

                TempData["Message"] = "Đã thêm sản phẩm vào giỏ hàng";
                
                return Redirect($"{url}");
            }
            catch (System.Exception)
            {
                
                throw;
            }
            
        }

        
    }


}