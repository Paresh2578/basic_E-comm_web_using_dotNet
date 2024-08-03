
using bulkyApp.Data;
using bulkyApp.Data.Interface;
using bulkyApp.Data.Repository;
using bulkyApp.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);



// Connect SQL
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBCS")));

// Interface
builder.Services.AddScoped<CategoryInterface, CategoryRepo>();
builder.Services.AddScoped<ProductInterface, ProductRepo>();
builder.Services.AddScoped<UserInterface, UserRepo>();
builder.Services.AddScoped<CardInterface, CardRepo>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Register custom services
builder.Services.AddScoped<UserRepo>();

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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
