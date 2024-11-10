using NETBACKING.CORE.APPLICATION.DTOs;
using NETBACKING.CORE.APPLICATION.Models;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Services;

public interface IUserService
{
    Task<List<UserModel>> GetAllUsers();
    Task<EditUserDto> GetUserById(string id);
    Task CreateUser(CreateUserDto userDto,string role);
    Task UpdateUser(EditUserDto userDto);
 
}