using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.DTOs;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Interfaces.Services;
using NETBACKING.CORE.APPLICATION.ViewModels;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(Roles.Admin.ToString()))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (User.IsInRole(Roles.Client.ToString()))
                {
                    return RedirectToAction("Index", "Client");
                }
                
            }

            return View(new LoginViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginDto = new LoginDTO
                {
                    Username = model.Username,
                    Password = model.Password
                };

                var result = await _accountService.LoginAsync(loginDto);
                if (result.IsSuccessful)
                {
                    return Redirect(result.RedirectUrl);
                }

                model.ErrorMessage = result.ErrorMessage;
            }

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Login");
        }
    
    }
}