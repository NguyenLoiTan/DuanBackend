using AdvancedEshop.Web.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

// Đăng ký IProductService trong container dịch vụ
/*builder.Services.AddHttpClient<IProductService, ProductService>(c =>
    c.BaseAddress = new Uri($"http://localhost:7136/")
);*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "categoryDetails",
        pattern: "Products/Details/{categoryId?}",
        defaults: new { controller = "Products", action = "Details" });
});



app.Run();
