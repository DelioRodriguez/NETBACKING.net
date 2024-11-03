using NETBACKING.CORE.APPLICATION.DTOs;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<LoginResultDTO> AuthenticateUserAsync(LoginDTO loginDto);
        Task SignOutAsync();
    }
}