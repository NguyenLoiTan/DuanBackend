
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using AdvancedEshop.Web.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddServerSideBlazor();




builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection"))
);
/*builder.Services.AddIdentity<IdentityUser, IdentityRole>()
     .AddDefaultTokenProviders();*/

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
app.UseSession();
app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();
app.MapBlazorHub();
/*app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");*/



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}");

    endpoints.MapControllerRoute(
        name: "admin",
        pattern: "Admin/{action=Index}",
        defaults: new { controller = "Admin" });
});



app.Run();
