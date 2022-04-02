using Electro_Project.Models.Context;
using Electro_Project.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Electro_Project.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDB"));
});

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<IdentityContext>();builder.Services.AddDbContext<IdentityContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDB"/*"IdentityContextConnection"*/)));

builder.Services.AddDefaultIdentity<AppUser>().AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ShopContext>();



// Custom Services
builder.Services.AddScoped<ILaptopService, LaptopService>();
builder.Services.AddScoped<IManufactureService, ManufactureService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Products}/{action=Index}/{id?}"
    
    );

app.MapRazorPages();
app.Run();
