using NETBACKING.CORE.APPLICATION.DTOs;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories;
using NETBACKING.CORE.APPLICATION.Interfaces.Services;
using NETBACKING.CORE.APPLICATION.Models;

namespace NETBACKING.CORE.APPLICATION.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
        var userModel = new UserModel
        {
            UserName = userDto.UserName,
            Password = userDto.Password,
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Identification = userDto.Identification,
            IsActive = true
        };

        await _userRepository.CreateUserAsync(userModel, role);
    }

    public async Task UpdateUser(EditUserDto userDto)
    {
        var userModel = new UserModel
        {
            Id = userDto.Id,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Identification = userDto.Identification,
            Email = userDto.Email,
            UserName = userDto.UserName
        };

        await _userRepository.UpdateUserAsync(userModel);
    }

    public async Task DeleteUser(string id)
    {
        await _userRepository.DeleteUserAsync(id);
    }
}