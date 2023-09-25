using LapTopShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");

    
});



// builder.Services.Configure<RazorViewEngineOptions>(options =>
// {
//     options.AreaViewLocationFormats.Clear();
//     options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
//     options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
//     options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
//     options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
// });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();




builder.Services.AddDbContext<dbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("ConnectDB") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");
    options.UseSqlServer(connectionString);
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<dbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login"; // Đặt trang đăng nhập mặc định
    // Các cấu hình khác ở đây...
});

// /Identity/Account/Login


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        areaName:"Admin",
        name: "admin",
        pattern: "{controller}/{action=Index}/{id?}");
        // pattern: "{controller=Home}/{action=Index}/{id?}");
        // {area:exists}/

    endpoints.MapAreaControllerRoute(
        areaName:"Admin",
        name: "admin",
        pattern: "{controller}/{action=Index}/{id?}");
    
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


});

app.MapRazorPages();

app.Run();
