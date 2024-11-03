using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NETBACKING.CORE.APPLICATION;
using NETBACKING.INFRAESTRUCTURE.IDENTITY;
using NETBACKING.INFRAESTRUCTURE.IDENTITY.Context;
using NETBACKING.INFRAESTRUCTURE.IDENTITY.Entities;
using NETBACKING.INFRAESTRUCTURE.IDENTITY.Seeds;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddContextInfrastructure(builder.Configuration);
builder.Services.AddApplicationService();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Inicializar roles y usuario administrador
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await DefaultRoles.SeedDefaultRolesAsync(userManager, roleManager);
    await DefaultAdminUser.SeedAsync(userManager);

    
}
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
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();