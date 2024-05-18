using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LapTopShop.Models;

namespace LapTopShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductManageController : Controller
    {
        private readonly dbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductManageController(dbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.Products.Include(p => p.Category);
            return View(await dbContext.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Status,TypeLaptop,ProductName,OldPrice,Discount,Price,Quantity,MainImageFile,ProductImageFiles,CpuTechnology,Kernel,Threads,ProcessorSpeedCPU,MaxSpeed,Caching,Ram,TypeRAM,RAMBusSpeed,MaxRam,HardDrive,ScreenSize,ScreenResolution,ScanFrequency,ScreenTechnology,GraphicCard,AudioTechnology,Connector,WirelessConnectivity,Webcam,KeyboardLight,Size,Weight,Material,Battery,Os,ReleaseTime,Article")] Product product)
        {
            // System.Console.WriteLine("CategoryId : "+product.CategoryId);
            // System.Console.WriteLine("Status : "+product.Status);
            // System.Console.WriteLine("TypeLaptop : "+product.TypeLaptop);
            // System.Console.WriteLine("ProductName : "+product.ProductName);
            // System.Console.WriteLine("OldPrice : "+product.OldPrice);
            // System.Console.WriteLine("Discount : "+product.Discount);
            // System.Console.WriteLine("Price : "+product.Price);
            // System.Console.WriteLine("Quantity : "+product.Quantity);
            // System.Console.WriteLine("MainImageFile : "+product.MainImageFile.FileName);
            // // System.Console.WriteLine("ProductImageFiles : "+product.ProductImageFiles);
            // System.Console.WriteLine("CpuTechnology : "+product.CpuTechnology);
            // System.Console.WriteLine("Kernel : "+product.Kernel);
            // System.Console.WriteLine("Threads : "+product.Threads);
            // System.Console.WriteLine("ProcessorSpeedCPU : "+product.ProcessorSpeedCPU);
            // System.Console.WriteLine("MaxSpeed : "+product.MaxSpeed);
            // System.Console.WriteLine("Caching : "+product.Caching);
            // System.Console.WriteLine("Ram : "+product.Ram);
            // System.Console.WriteLine("TypeRAM : "+product.TypeRAM);
            // System.Console.WriteLine("RAMBusSpeed : "+product.RAMBusSpeed);
            // System.Console.WriteLine("MaxRam : "+product.MaxRam);
            // System.Console.WriteLine("HardDrive : "+product.HardDrive);
            // System.Console.WriteLine("ScreenSize : "+product.ScreenSize);
            // System.Console.WriteLine("ScreenResolution : "+product.ScreenResolution);
            // System.Console.WriteLine("ScanFrequency : "+product.ScanFrequency);
            // System.Console.WriteLine("ScreenTechnology : "+product.ScreenTechnology);
            // System.Console.WriteLine("GraphicCard : "+product.GraphicCard);
            // System.Console.WriteLine("AudioTechnology : "+product.AudioTechnology);
            // System.Console.WriteLine("Connector : "+product.Connector);
            // System.Console.WriteLine("WirelessConnectivity : "+product.WirelessConnectivity);
            // System.Console.WriteLine("Webcam : "+product.Webcam);
            // System.Console.WriteLine("KeyboardLight : "+product.KeyboardLight);
            // System.Console.WriteLine("Size : "+product.Size);
            // System.Console.WriteLine("Weight : "+product.Weight);
            // System.Console.WriteLine("Material : "+product.Material);
            // System.Console.WriteLine("Battery : "+product.Battery);
            // System.Console.WriteLine("Os : "+product.Os);
            // System.Console.WriteLine("ReleaseTime : "+product.ReleaseTime);
            // System.Console.WriteLine("Article : "+product.Article);
            // System.Console.WriteLine("isvalid : "+ModelState);

            foreach (var key in ModelState.Keys)
            {
                var error = ModelState[key].Errors.FirstOrDefault();
                if (error != null)
                {
                    Console.WriteLine($"Field: {key}, Error: {error.ErrorMessage}");
                }
            }
            
            if (ModelState.IsValid)
            {
                System.Console.WriteLine("da vao : ");
                try
                {
                    int productId = -1;
                    
                    if (product.MainImageFile != null && product.ProductImageFiles != null)
                    {
                        System.Console.WriteLine("file name : "+product.MainImageFile.FileName);
                        System.Console.WriteLine("Đã lấy được ảnh từ MainImageFile. :  "+_env.WebRootPath);

                        var filePath = Path.Combine(_env.WebRootPath,"uploads",product.MainImageFile.FileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            // Nếu tệp đã tồn tại, bạn có thể xử lý ghi đè tệp tại đây.
                            System.Console.WriteLine("Tệp đã tồn tại. Thực hiện ghi đè.");

                            // Xóa tệp hiện có trước khi ghi đè
                            System.IO.File.Delete(filePath);
                        }

                        using var FileStream = new FileStream(filePath,FileMode.Create);
                        await product.MainImageFile.CopyToAsync(FileStream);
                        
                        var urlAvatar = Path.Combine("/uploads",product.MainImageFile.FileName);
                        // System.Console.WriteLine("Link anh : " + urlAvatar);

                        // Xử lí ảnh ở đây, ví dụ: lưu ảnh vào thư mục, đổi tên ảnh, v.v.
                        // Sau đó, bạn có thể lưu đường dẫn của ảnh vào cơ sở dữ liệu nếu cần.


                        // danh sach anh chi tiet
                        product.Avatar = urlAvatar;
                        

                    }else {
                         System.Console.WriteLine("Không có ảnh đại diện được chọn.");
                         throw new InvalidOperationException("Invalid");
                    }
                    
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    productId = product.Id;



                    if(product.ProductImageFiles != null && productId != -1){
                        foreach(var f in product.ProductImageFiles){
                            Img img = new Img();
                            var filePath = Path.Combine(_env.WebRootPath,"uploads",f.FileName);
                            if (System.IO.File.Exists(filePath))
                            {
                                // Nếu tệp đã tồn tại, bạn có thể xử lý ghi đè tệp tại đây.
                                System.Console.WriteLine("Tệp đã tồn tại. Thực hiện ghi đè.");

                                // Xóa tệp hiện có trước khi ghi đè
                                System.IO.File.Delete(filePath);
                            }

                            using var FileStream = new FileStream(filePath,FileMode.Create);
                            await f.CopyToAsync(FileStream);
                            var urlImg = Path.Combine("/uploads",f.FileName);

                            img.FilePath = urlImg;
                            img.ProductId = productId;
                            img.FileName = f.FileName;

                            _context.ProductImages.Add(img);
                            await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Không có ảnh chi tiết được chọn.");
                        throw new InvalidOperationException("Invalid");
                    }


                }
                catch (System.Exception)
                {
                    
                    throw;
                }
                
                return RedirectToAction(nameof(Index));
            }
            // System.comnsole.Writeline("isvalid : "+mode)
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,Status,ProductName,OldPrice,Discount,Price,Quantity,Avatar,CpuTechnology,Kernel,Threads,ProcessorSpeedCPU,MaxSpeed,Caching,Ram,TypeRAM,RAMBusSpeed,MaxRam,HardDrive,ScreenSize,ScreenResolution,ScanFrequency,ScreenTechnology,GraphicCard,AudioTechnology,Connector,WirelessConnectivity,Webcam,KeyboardLight,Size,Weight,Material,Battery,Os,ReleaseTime,Article")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'dbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.Id == id);
        }
    }
}
