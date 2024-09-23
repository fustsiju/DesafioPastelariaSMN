using System.Collections;
using System.Configuration;
using System.Net.Mail;
using System.Numerics;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Microsoft.AspNetCore.Identity;
using DesafioPastelariaSMN;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DBConexao>();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "Login.Cookie";
        config.LoginPath = "/Index";
    });


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

