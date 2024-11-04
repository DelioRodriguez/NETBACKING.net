using NETBACKING.CORE.APPLICATION.DTOs;
using NETBACKING.CORE.APPLICATION.Models;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<LoginResultDTO> AuthenticateUserAsync(LoginDTO loginDto);
        Task SignOutAsync();
        Task<List<UserModel>> GetAllUsersAsync();
        Task<EditUserDto> GetUserByIdAsync(string id);
        Task CreateUserAsync(UserModel userModel,string role);
        Task UpdateUserAsync(UserModel userModel);
        Task DeleteUserAsync(string id);
    }
}