using Microsoft.AspNetCore.Identity;
using NETBACKING.CORE.APPLICATION.DTOs;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories;
using NETBACKING.INFRAESTRUCTURE.IDENTITY.Entities;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginResultDTO> AuthenticateUserAsync(LoginDTO loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !user.IsActive)
            {
                return new LoginResultDTO
                {
                    IsSuccessful = false,
                    ErrorMessage = "Usuario no encontrado o inactivo. Contacte al administrador.",
                    IsActive = user?.IsActive ?? false
                };
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return new LoginResultDTO
                {
                    IsSuccessful = false,
                    ErrorMessage = "Usuario o contraseña incorrectos."
                };
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            var roles = await _userManager.GetRolesAsync(user);
            string redirectUrl = roles.Contains(Roles.Admin.ToString()) ? "/Admin/Home" : "/Client/Home";

            return new LoginResultDTO
            {
                IsSuccessful = true,
                IsActive = true,
                RedirectUrl = redirectUrl
            };
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}