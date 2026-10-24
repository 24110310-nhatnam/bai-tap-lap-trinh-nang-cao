using Microsoft.EntityFrameworkCore;
using QLTV.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QLTVDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLTVConnection")));

var app = builder.Build();

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
    pattern: "{controller=Sach}/{action=Index}/{id?}");

app.Run();
