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

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> IsEmailDuplicateAsync(string email, string userId)
        {
            return await _userRepository.IsEmailDuplicateAsync(email, userId);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.EmailExistsAsync(email);
        }

        public async Task<bool> IdentificationExistsAsync(string identification)
        {
            return await _userRepository.IdentificationExistsAsync(identification);

        }

        public async  Task<bool> UsernameExistsAsync(string username)
        {
            return await _userRepository.UsernameExistsAsync(username);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<EditUserDto> GetUserById(string id)
        {
            
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task CreateUser(CreateUserDto userDto, string role)
        {
            
            if (!Enum.TryParse(role, true, out Roles parsedRole))
            {
                throw new ArgumentException("Invalid role specified.");
            }

           
            await _userRepository.CreateUser(userDto, role);

           
        }

        public async Task UpdateUser(EditUserDto userDto)
        {
        
            var userModel = _mapper.Map<UserModel>(userDto);

            await _userRepository.UpdateUserAsync(userModel);
        }
    }
}
