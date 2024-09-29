using Microsoft.EntityFrameworkCore;
using PV221Chat.Core.DataContext;
using PV221Chat.Core.Services;
using Microsoft.AspNetCore.SignalR;
using PV221Chat.Hubs;
using PV221Chat.Core.Repositories;
using PV221Chat.Core.Repositories.EF;
using PV221Chat.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add service DJ Interfaces to Repositories
builder.Services.AddRepositoryService();

// Add connection string to db context
builder.Services.AddDbContext<Pv221chatContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IChatRepository, ChatRepository>();

// Add SignalR
builder.Services.AddSignalR();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chatHub");

app.Run();
