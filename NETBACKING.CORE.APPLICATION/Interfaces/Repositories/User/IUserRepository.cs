using NETBACKING.CORE.APPLICATION.DTOs;
using NETBACKING.CORE.APPLICATION.Models;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Repositories
{
    public interface IUserRepository
    { 
        Task<bool> IsEmailDuplicateAsync(string email, string userId);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> IdentificationExistsAsync(string identification);
        Task<bool> UsernameExistsAsync(string username);
        Task<LoginResultDTO> AuthenticateUserAsync(LoginDTO loginDto);
        Task SignOutAsync();
        Task<List<UserModel>> GetAllUsersAsync();
        Task<EditUserDto> GetUserByIdAsync(string id);
        Task CreateUser(CreateUserDto userDto, string role);
        Task UpdateUserAsync(UserModel userModel);
        Task DeleteUserAsync(string id);
    }
}