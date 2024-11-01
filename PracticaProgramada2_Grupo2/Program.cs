using PracticaProgramada2_Grupo2.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add conecction server
// "ConnectionString": {
//    "Minombredeconexion": "Server=srv863.hstgr.io;Port=3306;User=u484426513_pac324;Password=B&XWouC#9Ef;Database=u484426513_pac324;"
//  }
var connectionString = builder.Configuration.GetConnectionString("Minombredeconexion");

// Add to the service
builder.Services.AddDbContext<MinombredeconexionDbContext>(
    options =>
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))
    ));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
