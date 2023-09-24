using LapTopShop.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// builder.Services.Areas

// builder.Services.AddControllersWithViews()
//     .AddRazorOptions(options =>
//     {
//         // Specify the Area root directory
//         options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
//         options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
//         options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
//         options.AreaViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
//         // ... (add other view location formats as needed)
//     });

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




builder.Services.AddDbContext<dbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("ConnectDB") ?? "";
    options.UseSqlServer(connectionString);
});

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

// app.Map("/admin", app1 =>
// {
//     app1.UseEndpoints(endpoints =>
//     {
//         endpoints.MapControllerRoute(
//             name: "admin",
//             pattern: "admin/{controller=Home}/{action=Index}/{id?}");
//     });
// });


// app.UseEndpoints(endpoints =>
// {
    

//     endpoints.MapControllerRoute(
//         name: "default",
//         pattern: "{controller=Home}/{action=Index}/{id?}");
// });


app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        areaName:"Admin",
        name: "admin",
        pattern: "{controller}/{action=Index}/{id?}");
        // pattern: "{controller=Home}/{action=Index}/{id?}");
        // {area:exists}/
    
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


});

app.Run();
