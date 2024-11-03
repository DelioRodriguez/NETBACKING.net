using NETBACKING.CORE.APPLICATION.DTOs;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories;
using NETBACKING.CORE.APPLICATION.Interfaces.Services;

namespace NETBACKING.CORE.APPLICATION.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<LoginResultDTO> LoginAsync(LoginDTO loginDto)
        {
            return await _userRepository.AuthenticateUserAsync(loginDto);
        }

        public async Task LogoutAsync()
        {
            await _userRepository.SignOutAsync();
        }
    }
}