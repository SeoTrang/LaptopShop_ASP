using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LapTopShop.Models;
using Microsoft.EntityFrameworkCore;
using LapTopShop.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LapTopShop.Controllers
{
    // [Route("[controller]")]
    [Authorize]
    public class OrderController : Controller
    {
         private readonly dbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<OrderController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(
            dbContext context,
            IWebHostEnvironment env,
            ILogger<OrderController> logger,
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
            System.Console.WriteLine("user is : ");
            System.Console.WriteLine(user);
            System.Console.WriteLine(user.Id);
            if (user == null)
            {
                return Redirect("/login");
            }
            // var carts = await _context.Carts
            //     .Where(c => c.UserId == user.Id)
            //     .Include(c => c.CartItems)
            //     .ThenInclude(ci => ci.Product)
            //     .FirstOrDefaultAsync();

            var orders = await _context.Orders
            .Where(o => o.UserId == user.Id)
            .FirstOrDefaultAsync();

            if (orders == null)
            {
                // Nếu không tìm thấy giỏ hàng, bạn có thể thực hiện xử lý tương ứng ở đây
                // Ví dụ: trả về một trang thông báo không có giỏ hàng
                return View();
            }
            ViewBag.Order = orders;

            Console.WriteLine(orders);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet]
        public async Task<IActionResult> FormInput(int? CartId,int? TotalPrice){
            _logger.LogInformation($"CartId={CartId}");
            _logger.LogInformation($"TotalPrice={TotalPrice}");
            ViewBag.CartId = CartId;
            ViewBag.TotalPrice = TotalPrice;

            // if()
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FormInput(int CartId,int TotalPrice,[Bind("Receiver,Email,Phone,Address, Note")] Order order){
            var user = await _userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return Redirect("/login");
            }
            var cart = _context.Carts.FirstOrDefault(p => p.Id == CartId);
            if (ModelState.IsValid)
            {
                int orderId = -1;
                order.UserId = user.Id;

                Random random = new Random();
                int argument1 = random.Next(1, 101); // Tạo một số ngẫu nhiên từ 1 đến 100
                int argument2 = random.Next(1, 101); // Tạo một số ngẫu nhiên từ 1 đến 100



                string code = "#" + UniqueCodeGenerator.GenerateUniqueCode(argument1, argument2);
                _logger.LogInformation($"Code: {code}");
                order.Code = code;
                order.State = "Đang Chờ";
                // order.State = "Đang Giao";
                // order.State = "Đã Giao";
                DateTime dateTime = new DateTime();
                // order.Date = dateTime.ToString("dd/MM/yyyy");
                order.Date = dateTime;
                order.TotalPrice = TotalPrice;

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                orderId = order.Id;

                // _logger.LogInformation($"order.UserId: {order.UserId}");
                // _logger.LogInformation($"order.Code: {order.Code}");
                // _logger.LogInformation($"order.Receiver: {order.Receiver}");
                // _logger.LogInformation($"order.Address: {order.Address}");
                // _logger.LogInformation($"order.Phone: {order.Phone}");
                // _logger.LogInformation($"order.Email: {order.Email}");
                // _logger.LogInformation($"order.Note: {order.Note}");
                // _logger.LogInformation($"order.State: {order.State}");
                // _logger.LogInformation($"order.Date: {order.Date}");
                // _logger.LogInformation($"order.TotalPrice: {order.TotalPrice}");


                // order items 
                
                // var cartItem = await _context.CartItems
                //         .Where(c => c.CartId == CartId)
                //         .Include(c => c.Product)
                //         .ToListAsync();  
                var cartItem = _context.CartItems
                        .Where(c => c.CartId == CartId)
                        .ToList();
                

                
                // _logger.LogInformation($"cartItem.Count: {cartItem.Count}");
                // // _logger.LogInformation($"cartItem.Product: {cartItem.Product.ProductName}");
               
                foreach (var item in cartItem)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = orderId;
                    _logger.LogInformation("CartItem : " + item.Id);
                    // _logger.LogInformation("CartItem product name : " + item.Product.Id);
                    var productItem = _context.Products
                        .Where(p=>p.Id == item.ProductId)
                        .FirstOrDefault();
                    orderItem.OrderId = orderId;
                    orderItem.ProductId = productItem.Id;
                    orderItem.Quantity = cartItem.Count;
                    orderItem.Count = cartItem.Count;
                    orderItem.ProductName = productItem.ProductName;
                    orderItem.Avatar = productItem.Avatar;
                    orderItem.Price = productItem.Price;
                    orderItem.Status = productItem.Status;
                  
                    _logger.LogInformation("Product name : " + productItem.ProductName);
                    await _context.OrderItems.AddAsync(orderItem);
                    await _context.SaveChangesAsync();


                    item.Product = productItem;
                    
                }

                var cartDel = _context.Carts
                    .Include(c => c.CartItems)
                    .Where(c => c.Id == CartId)
                    .FirstOrDefault(); // Thay thế FirstOrDefaultAsync bằng ToListAsync

                _logger.LogInformation("cartDel : " + cartDel.Id);
                _logger.LogInformation("cartDel[1] : " + cartDel.CartItems[1].Id);
                if (cartDel != null)
                {
                    _logger.LogInformation("da vao ");
                    _context.Carts.Remove(cartDel); // Xóa Cart cùng với các CartItems liên quan
                    await _context.SaveChangesAsync();
                }


                // foreach(var item in orderItem){

                // }

                // foreach (var item in cartItem)
                // {
                //     _logger.LogInformation("CartItem : " + item.Id);
                //     _logger.LogInformation("CartItem product name : " + item.Product.ProductName);
                // }



                

            }
            return Redirect("/");
            // return Content("hello world");
        }
    }
}