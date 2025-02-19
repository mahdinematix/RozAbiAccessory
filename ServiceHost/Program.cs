using _01_Framework.Application;
using BlogManagement.Infrastructure.Configuration;
using ServiceHost;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using _01_Framework.Application.Email;
using _01_Framework.Application.Sms;
using _01_Framework.Application.ZarinPal;
using _01_Framework.Infrastructure;
using AccountManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Infrastructure.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.
services.AddRazorPages()
    .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Discount", "Discount");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
    });

var connectionString = builder.Configuration.GetConnectionString("RozAbiAccessoryDb");

ShopManagementBootstrapper.Configure(services, connectionString); 
DiscountManagementBootstrapper.Configure(services, connectionString);
InventoryManagementBootstrapper.Configure(services, connectionString);
BlogManagementBootstrapper.Configure(services, connectionString);
CommentManagementBootstrapper.Configure(services, connectionString);
AccountManagementBootstrapper.Configure(services, connectionString);
services.AddTransient<IPasswordHasher, PasswordHasher>();
services.AddTransient<IFileUploader, FileUploader>();
services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
services.AddHttpContextAccessor();
services.AddTransient<IAuthHelper, AuthHelper>();
services.AddTransient<ISmsService, SmsService>();
services.AddTransient<IEmailService, EmailService>();

services.Configure<CookieTempDataProviderOptions>(options => {
    options.Cookie.IsEssential = true;
});

services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });

services.AddAuthorization(options =>
{
    options.AddPolicy("AdminArea",
        builder => builder.RequireRole(new List<string> { Roles.Administrator, Roles.ContentUploader }));

    options.AddPolicy("Shop",
        builder => builder.RequireRole(new List<string> { Roles.Administrator }));

    options.AddPolicy("Discount",
        builder => builder.RequireRole(new List<string> { Roles.Administrator }));

    options.AddPolicy("Account",
        builder => builder.RequireRole(new List<string> { Roles.Administrator }));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
