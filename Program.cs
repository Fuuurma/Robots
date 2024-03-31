using Microsoft.AspNetCore.Routing.Constraints;
using UF2_Robots.Models.Database;

var builder = WebApplication.CreateBuilder(args);

var dbName = "Factory";

builder
    .Services
    .AddTransient<DatabaseConnection>(
        provider => new DatabaseConnection(dbName));

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
    pattern: "{controller=Home}/{action=Index}");

app.MapControllerRoute(
    name: "delete",
    pattern: "{controller=Home}/{action=Delete}/{tableName}/{id?}", 
    defaults: new { controller = "Home", action = "Delete" },
    constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }); 

app.MapControllerRoute(
    name: "create",
    pattern: "{controller=Home}/{action=Create}",
    defaults: new { controller = "Home", action = "Create" },
    constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") });

app.MapControllerRoute(
    name: "reset",
    pattern: "{controller=Home}/{action=Reset}",
    defaults: new { controller = "Home", action = "Reset" },
    constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }); 

app.MapControllerRoute(
    name: "update",
    pattern: "{controller=Home}/{action=Update}",
    defaults: new { controller = "Home", action = "Update" },
    constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") }); 

app.Run();
