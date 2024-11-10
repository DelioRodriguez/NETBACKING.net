using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NETBACKING.CORE.APPLICATION.DTOs;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.Models;
using NETBACKING.INFRAESTRUCTURE.IDENTITY.Entities;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , RoleManager<IdentityRole> roleManager, IMapper mapper, IProductService productService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _productService = productService;
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
                UserRole = roles.FirstOrDefault()
            };
        }


        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userList = new List<UserModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user); 
                var role = roles.FirstOrDefault(); 

                userList.Add(new UserModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Identification = user.Identification,
                    Email = user.Email,
                    UserName = user.UserName,
                    IsActive = user.IsActive,
                    Role = role 
                });
            }

            return userList;
        }


        public async Task<EditUserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null!;

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

        public async Task CreateUser(CreateUserDto userDto, string role)
        {
            // Validación del rol
            if (!Enum.TryParse(role, true, out Roles parsedRole))
            {
                throw new ArgumentException("Invalid role specified.");
            }

            var user = new ApplicationUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Identification = userDto.Identification,
                IsActive = userDto.IsActive
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Error creating user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            if (string.IsNullOrEmpty(user.Id))
            {
                throw new InvalidOperationException("User ID not assigned after user creation.");
            }

            // Asignar el rol
            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException("Error assigning role: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }

            // Si el rol es 'Client', crear la cuenta de ahorro
            if (parsedRole == Roles.Client)
            {
                var savingsAccount = _mapper.Map<ProductCreateViewModel>(userDto);

                // Asegurar que ApplicationUserId esté asignado
                savingsAccount.ApplicationUserId = user.Id;  // Asignar el Id generado
                savingsAccount.Balance = userDto.InitialAmount ?? 0;

                // Verificar que ApplicationUserId no sea nulo
                if (string.IsNullOrEmpty(savingsAccount.ApplicationUserId))
                {
                    throw new InvalidOperationException("ApplicationUserId cannot be null when creating a product.");
                }

                await _productService.CreateProduct(savingsAccount);
            }
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