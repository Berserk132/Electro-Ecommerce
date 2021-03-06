using Electro_Project.Models.Context;
using Electro_Project.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Electro_Project.Areas.Identity.Data;
using Electro_Project.Models.Cart;
using Electro_Project.Models.PaymentSettings;
using Electro_Project.Models.Mail.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDB"));
});


// enable cors
var MyAllowSpecificOrigins = "";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<IdentityContext>();builder.Services.AddDbContext<IdentityContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDB"/*"IdentityContextConnection"*/)));

builder.Services.AddDefaultIdentity<AppUser>().AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ShopContext>();


// Session Initiallizer
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));


//Payment Services
//PayPal
builder.Services.Configure<PayPalSettings>(builder.Configuration.GetSection("PayPal"));


// Custom Services
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<ILaptopService, LaptopService>();
builder.Services.AddScoped<IMobileService, MobileService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IWishListService, WishListService>();
builder.Services.AddScoped<IManufactureService, ManufactureService>();



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddHttpClient();


// add mail services
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
//builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<IEmailSender, MailService>();


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
app.UseSession();
app.UseCors(MyAllowSpecificOrigins);

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Products}/{action=Index}/{id?}"

    );

app.MapRazorPages();
app.Run();
