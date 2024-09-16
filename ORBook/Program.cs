using ORBook.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORBook.Data;
using ORBook.Models;
using ORBook.Service;
using ORBook.Service.BookGenres;
using ORBook.Service.Books;
using ORBook.Service.BookUsers;
using ORBook.Service.Genres;
using ORBook.Service.Notifications;
using ORBook.Service.Users;
using ORBook.Service.Volumns;
using ORBook.Hubs;
using ContosoUniversity.Data;
using FluentValidation;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ORBookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ORBookContext") ?? throw new InvalidOperationException("Connection string 'ORBookContext' not found.")));

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // This ensures the app listens on port 80
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add SignalR
builder.Services.AddSignalR();

// Service
builder.Services.AddScoped<ICommonDataService<Book>, BookService>();
builder.Services.AddScoped<ICommonDataService<Genre>, GenreService>();
builder.Services.AddScoped<ICommonDataService<Volumn>, VolumnService>();
builder.Services.AddScoped<ICommonDataService<User>, UserService>();

builder.Services.AddScoped<IBookGenreService, BookGenreService>();
builder.Services.AddScoped<IBookUserService, BookUserService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Configure
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Users/Login"; // Đường dẫn khi chưa đăng nhập
                options.AccessDeniedPath = "/Users/AccessDenied"; // Đường dẫn khi bị từ chối truy cập
            });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<NotificationHub>("/notificationHub");
app.Run();

