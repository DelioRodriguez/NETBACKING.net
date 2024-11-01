using Microsoft.AspNetCore.Identity;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.INFRAESTRUCTURE.IDENTITY.Entities;

namespace NETBACKING.INFRAESTRUCTURE.IDENTITY.Seeds;

public static class DefaultRoles
{
    
    public static async Task SeedDefaultRolesAsync(UserManager<ApplicationUser> manager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Client.ToString()));
    }
}