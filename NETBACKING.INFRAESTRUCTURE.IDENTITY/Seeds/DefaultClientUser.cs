using Microsoft.AspNetCore.Identity;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.INFRAESTRUCTURE.IDENTITY.Entities;

namespace NETBACKING.INFRAESTRUCTURE.IDENTITY.Seeds;

public static class DefaultClientUser
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
    {
        ApplicationUser clientUser = new()
        {
            UserName = "client",
            Email = "client@email.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            FirstName = "Client",
            LastName = "User",
            Identification = "C987654321",
            IsActive = true,
            InitialAmount = 500
        };

        if (userManager.Users.All(u => u.UserName != clientUser.UserName))
        {
            var user = await userManager.FindByEmailAsync(clientUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(clientUser, "Client123!");
                await userManager.AddToRoleAsync(clientUser, Roles.Client.ToString());
            }
        }
    }
    
}