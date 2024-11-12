using NETBACKING.CORE.APPLICATION.DTOs;
using NETBACKING.CORE.APPLICATION.Models;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Services;

public interface IUserService
{
    Task<bool> IsEmailDuplicateAsync(string email, string userId);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> IdentificationExistsAsync(string identification);
    Task<bool> UsernameExistsAsync(string username);
    Task<List<UserModel>> GetAllUsers();
    Task<EditUserDto> GetUserById(string id);
    Task CreateUser(CreateUserDto userDto,string role);
    Task UpdateUser(EditUserDto userDto);

}