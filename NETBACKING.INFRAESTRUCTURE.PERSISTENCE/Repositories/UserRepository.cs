using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NETBACKING.CORE.APPLICATION.DTOs;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories;
using NETBACKING.CORE.APPLICATION.Models;
using NETBACKING.INFRAESTRUCTURE.IDENTITY.Entities;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
                RedirectUrl = redirectUrl,
                UserRole = roles.FirstOrDefault() // Asignar el rol del usuario aquí
            };
        }


        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Select(user => new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Identification = user.Identification,
                Email = user.Email,
                UserName = user.UserName,
                IsActive = user.IsActive
            }).ToList();
        }

        public async Task<EditUserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            return new EditUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Identification = user.Identification,
                Email = user.Email,
                UserName = user.UserName,
                IsActive = user.IsActive
            };
        }

        public async Task CreateUserAsync(UserModel userModel, string role)
        {
            var user = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Identification = userModel.Identification,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                throw new Exception("No se pudo crear el usuario: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // Verificar si el rol existe, de lo contrario lanzar una excepción
            if (!await _roleManager.RoleExistsAsync(role))
            {
                throw new Exception($"El rol '{role}' no existe.");
            }

            // Asignar el rol al usuario
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task UpdateUserAsync(UserModel userModel)
        {
            var user = await _userManager.FindByIdAsync(userModel.Id);
            if (user == null) throw new Exception("Usuario no encontrado");

            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Identification = userModel.Identification;
            user.IsActive = userModel.IsActive;
            user.Email = userModel.Email;
            user.UserName = userModel.UserName;

            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            else
            {
                throw new Exception("Usuario no encontrado");
            }
        }
    }
}