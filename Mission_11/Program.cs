using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mission_11.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BookstoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:connection"]);
});

builder.Services.AddScoped<IBookInterface, EFBookRepository>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:IdentityConnection"]));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();

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

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute("pagenumandtype", "{bookType}/{pageNum}", new { Controller = "Home", action = "Index" });
//app.MapControllerRoute("pagination", "{pageNum}", new { Controller = "Home", Action = "Index", pageNum = 1 });
//app.MapControllerRoute("bookType", "{bookType}", new { Controller = "Home", action = "Index", pageNum = 1 });

app.MapControllerRoute("pagenumandtype", "{bookType}/Page{pageNum}", new { Controller = "Home", Action = "Index" });
app.MapControllerRoute("page", "Page/{pageNum}", new { Controller = "Home", Action = "Index", pageNum = 1 });
app.MapControllerRoute("bookType", "{bookType}", new { Controller = "Home", Action = "Index", pageNum = 1 });
app.MapControllerRoute("pagination", "Books/Page{pageNum}", new { Controller = "Home", Action = "Index", pageNum = 1 });


app.MapDefaultControllerRoute();
app.MapRazorPages();
IdentitySeedData.EnsurePopulated(app);
app.Run();
