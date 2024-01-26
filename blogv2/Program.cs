using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using blogv2.Data;
using Microsoft.AspNetCore.Identity;
using blogv2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<blogv2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("blogv2Context") ?? throw new InvalidOperationException("Connection string 'blogv2Context' not found.")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Home/";
    });
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.MapControllerRoute(
    name: "profile",
    pattern: "/{username}",
    defaults: new { controller = "Profile", action = "Index" }
);




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
