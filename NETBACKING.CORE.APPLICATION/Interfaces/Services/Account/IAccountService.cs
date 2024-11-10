using NETBACKING.CORE.APPLICATION.DTOs;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Services;

public interface IAccountService
{
    Task<LoginResultDTO> LoginAsync(LoginDTO loginDto);
    Task LogoutAsync();
}