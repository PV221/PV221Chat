using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PV221Chat.Core.DataContext;
using PV221Chat.Core.Services;
using PV221Chat.SignalR;
using PV221Chat.Services;

var builder = WebApplication.CreateBuilder(args);

// Add service DJ Interfaces to Repositories
builder.Services.AddRepositoryService();
builder.Services.AddService();

builder.Services.AddSignalR();

// Add connection string to db context
builder.Services.AddDbContext<Pv221chatContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
          {
              options.LoginPath = "/Login/Login"; // Сторінка логіну
              options.LogoutPath = "/Login/Logout"; // Сторінка логауту
              options.AccessDeniedPath = "/Login/AccessDenied"; // Сторінка доступу заборонено
              options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Час життя cookie
              options.SlidingExpiration = true; // Автоматичне продовження часу життя cookie
          })
          .AddGoogle(googleOptions =>
          {
              googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
              googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
          })
          .AddGitHub(githubOptions =>
          {
              githubOptions.ClientId = builder.Configuration["Authentication:GitHub:ClientId"];
              githubOptions.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"];
          });

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");
app.MapHub<ChatListHub>("/chatListHub");

app.MapHub<GlobalChatHub>("/globalChatHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
