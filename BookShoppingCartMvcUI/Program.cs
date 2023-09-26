using BookShoppingCartMvcUI;
using BookShoppingCartMvcUI.Configurations;
using BookShoppingCartMvcUI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = DefaultConnectionService.GetConnectionString(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(connectionString));




//DefaultConnection

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("DATABASE_URL");
//var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(connectionString));

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IHomeRepository,HomeRepository>();
builder.Services.AddTransient<ICartRepository,CartRepository>();
builder.Services.AddTransient<IUserOrderRepository,UserOrderRepository>();
builder.Services.AddScoped<IEmailSender, EmailService>();

// Register pre-configured instance of MailSettings class
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

// Register EmailService
//builder.Services.AddScoped<ShoppingCartEmailSender, EmailService>();


var app = builder.Build();

// UNCOMMENT ONCE YOU HAVE THE DATABASE WORKING

using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedDefaultData(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
