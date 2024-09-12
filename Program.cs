using CRMV2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;
using CRMV2.Ops;
using Hangfire;
using CRMV2.ViewModels;

var builder = WebApplication.CreateBuilder(args);
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

// Add services to the container.
builder.Services.AddControllersWithViews();
var hangfireConnectionString = ("Data Source=DESKTOP-CPS5QP0;Initial Catalog=HangFireDb;User ID=sa;Password=9984;TrustServerCertificate=True;");
builder.Services.AddDbContext<CrmContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
        options.Cookie.Name = "SRM2Auth";
    });

builder.Services.AddHangfire(x =>
{
    x.UseSqlServerStorage(hangfireConnectionString);
    RecurringJob.AddOrUpdate<ContractUpdater>(j => j.UpdateExpiredContracts(), "00 00 * * *");
});
builder.Services.AddHangfireServer();
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

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Users}/{action=Login}/{id?}");

app.Run();
