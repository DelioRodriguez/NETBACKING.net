using Microsoft.AspNetCore.Identity;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.INFRAESTRUCTURE.IDENTITY.Entities;

namespace NETBACKING.INFRAESTRUCTURE.IDENTITY.Seeds;

public class DefaultAdminUser
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
    {
        ApplicationUser adminUser = new()
        {
            UserName = "admin",
            Email = "admin@email.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            FirstName = "Admin",
            LastName = "User",
            Identification = "A123456789",
            IsActive = true,
            InitialAmount = 1000
        };

        if (userManager.Users.All(u => u.UserName != adminUser.UserName))
        {
            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(adminUser, "Admin123!");
                await userManager.AddToRoleAsync(adminUser, Roles.Admin.ToString());
            }
        }
    }
}