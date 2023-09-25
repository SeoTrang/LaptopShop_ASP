using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LapTopShop.Models;


namespace LapTopShop.Controllers
{
    // [Area("Admin")]

    [Route("/admin/[controller]/[action]/{id?}")]
    // [Route("/admin/[action]/{id?}")]
    public class CategoryController : Controller
    {
        private readonly dbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(dbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Category
   
        // [Route("/admin/index")]
        public async Task<IActionResult> Index()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'dbContext.Categories'  is null.");
        }

        // GET: Category/Details/5
        // [Route("/admin/details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            System.Console.WriteLine("test");
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,MainImageFile,Avatar")] Category category)
        {

            if (category.MainImageFile != null)
            {
                System.Console.WriteLine("file name : "+category.MainImageFile.FileName);
                System.Console.WriteLine("Đã lấy được ảnh từ MainImageFile. :  "+_env.WebRootPath);

                var filePath = Path.Combine(_env.WebRootPath,"uploads",category.MainImageFile.FileName);
                if (System.IO.File.Exists(filePath))
                {
                    // Nếu tệp đã tồn tại, bạn có thể xử lý ghi đè tệp tại đây.
                    System.Console.WriteLine("Tệp đã tồn tại. Thực hiện ghi đè.");

                    // Xóa tệp hiện có trước khi ghi đè
                    System.IO.File.Delete(filePath);
                }

                using var FileStream = new FileStream(filePath,FileMode.Create);
                await category.MainImageFile.CopyToAsync(FileStream);

                var urlAvatar = Path.Combine("/uploads",category.MainImageFile.FileName);
                System.Console.WriteLine("Link anh : " + urlAvatar);
                if(string.IsNullOrEmpty(urlAvatar)) return NotFound();
                

                // Xử lí ảnh ở đây, ví dụ: lưu ảnh vào thư mục, đổi tên ảnh, v.v.
                // Sau đó, bạn có thể lưu đường dẫn của ảnh vào cơ sở dữ liệu nếu cần.
                category.Avatar = urlAvatar;

                // return View();
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View();
            // if (ModelState.IsValid)
            // {
                // _context.Add(category);
                // await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
            // }
            // return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Avatar")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'dbContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
