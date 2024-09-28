using Microsoft.EntityFrameworkCore;
using PV221Chat.Core.DataContext;
using PV221Chat.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add service DJ Interfaces to Repositories
builder.Services.AddRepositoryService();

// Add connection string to db context
builder.Services.AddDbContext<Pv221chatContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
