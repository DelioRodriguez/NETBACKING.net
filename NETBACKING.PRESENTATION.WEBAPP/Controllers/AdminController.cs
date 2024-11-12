using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Interfaces.Services;
using NETBACKING.CORE.APPLICATION.DTOs;
using AutoMapper;
using NETBACKING.CORE.APPLICATION.Models;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class AdminController : Controller
    {
        private readonly IDashBoardService _dashboardService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdminController(IDashBoardService dashboardService, IUserService userService, IMapper mapper)
        {
            _dashboardService = dashboardService;
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var dashboardData = _dashboardService.GetDashboardData();
            return View(dashboardData);
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllUsers(); 
            return View(users);
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            userDto.IsActive = true;

            if (ModelState.IsValid)
            {
                
                if (await _userService.EmailExistsAsync(userDto.Email))
                {
                    ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");
                }

                
                if (await _userService.IdentificationExistsAsync(userDto.Identification))
                {
                    ModelState.AddModelError("Identification", "La Cedula ya está registrada.");
                }

            
                if (await _userService.UsernameExistsAsync(userDto.UserName))
                {
                    ModelState.AddModelError("Username", "El nombre de usuario ya está registrado.");
                }

               
                if (!ModelState.IsValid)
                {
                    return View(userDto);
                }

              
                await _userService.CreateUser(userDto, userDto.UserType);
                return RedirectToAction(nameof(Users));
            }

            return View(userDto);
        }


        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.GetUserById(id); 
          


            if (user == null)
            {
                return NotFound();
            }

            var editUserDto = _mapper.Map<EditUserDto>(user);
         

            return View(editUserDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userService.GetUserById(userDto.Id);
                if (existingUser == null)
                {
                    ModelState.AddModelError(string.Empty, "El usuario no existe.");
                    return View(userDto);
                }

                if (userDto.Email != existingUser.Email)
                {
                    if (await _userService.IsEmailDuplicateAsync(userDto.Email, userDto.Id))
                    {
                        ModelState.AddModelError("Email", "El correo electrónico ya está registrado en otro usuario.");
                        return View(userDto);
                    }
                }

                await _userService.UpdateUser(userDto);
                return RedirectToAction(nameof(Users)); 
            }

            return View(userDto);
        }


        [HttpPost]
        public async Task<IActionResult> ToggleUserStatus(string id)
        {
           
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            user.IsActive = !user.IsActive;

            await _userService.UpdateUser(_mapper.Map<EditUserDto>(user));

            return RedirectToAction(nameof(Users));
        }
    }
}
