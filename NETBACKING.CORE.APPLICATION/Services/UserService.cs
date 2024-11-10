using AutoMapper;
using NETBACKING.CORE.APPLICATION.DTOs;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories;
using NETBACKING.CORE.APPLICATION.Interfaces.Services;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.Models;
using NETBACKING.CORE.APPLICATION.Enums;

namespace NETBACKING.CORE.APPLICATION.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public UserService(IUserRepository userRepository, IProductService productService, IMapper mapper)
        {
            _userRepository = userRepository;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<EditUserDto> GetUserById(string id)
        {
            // Devuelve los datos de un usuario por su Id
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task CreateUser(CreateUserDto userDto, string role)
        {
            // Validación del rol
            if (!Enum.TryParse(role, true, out Roles parsedRole))
            {
                throw new ArgumentException("Invalid role specified.");
            }

            // Crear el usuario mediante el repositorio
            await _userRepository.CreateUser(userDto, role);

           
        }

        public async Task UpdateUser(EditUserDto userDto)
        {
        
            var userModel = _mapper.Map<UserModel>(userDto);

            await _userRepository.UpdateUserAsync(userModel);
        }

      

        public async Task<LoginResultDTO> AuthenticateUser(LoginDTO loginDto)
        {
         
            return await _userRepository.AuthenticateUserAsync(loginDto);
        }

        public async Task SignOut()
        {
          
            await _userRepository.SignOutAsync();
        }
    }
}
