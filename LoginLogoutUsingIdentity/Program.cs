using LoginLogoutUsingIdentity.Data;
using LoginLogoutUsingIdentity.Models;
using LoginLogoutUsingIdentity.Services;
using LoginLogoutUsingIdentity.Services.Abstration;
using LoginLogoutUsingIdentity.Services.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//registercontext
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("connect")));

//Register IdentityUser ie:ApplicationUser   and IdentityRole
builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

//For cookies
builder.Services.ConfigureApplicationCookie(options =>
options.LoginPath = "/UserAuthenticate/Login");


//Register service
builder.Services.AddScoped<IAccountService, AccountService>();

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
//Add authen and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserAuthenticate}/{action=Login}/{id?}");

app.Run();
