using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Interfaces.Services;
using NETBACKING.CORE.APPLICATION.DTOs;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class AdminController : Controller
    {
        private readonly IDashBoardService _dashboardService;
        private readonly IUserService _userService; // Inyección del servicio de usuario

        public AdminController(IDashBoardService dashboardService, IUserService userService)
        {
            _dashboardService = dashboardService;
            _userService = userService; // Inicialización del servicio de usuario
        }

        public IActionResult Index()
        {
            var dashboardData = _dashboardService.GetDashboardData();
            return View(dashboardData);
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllUsers(); // Obtener todos los usuarios
            return View(users); // Pasar los usuarios a la vista
        }

        public IActionResult CreateUser()
        {
            return View(); // Mostrar vista para crear un nuevo usuario
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            userDto.IsActive = true;
            if (ModelState.IsValid)
            {
                await _userService.CreateUser(userDto,userDto.UserType); // Llamar al servicio para crear el usuario
                return RedirectToAction(nameof(Index)); // Redirigir al listado de usuarios
            }
            return View(userDto); // Volver a mostrar el formulario si hay errores
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.GetUserById(id); // Obtener el usuario del servicio

            if (user == null)
            {
                return NotFound();
            }

            // Mapea UserModel a EditUserDto
            var editUserDto = new EditUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Identification = user.Identification,
                UserName = user.UserName,
                IsActive = user.IsActive
            };

            return View(editUserDto);
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUser(userDto); // Llamar al servicio para actualizar el usuario
                return RedirectToAction(nameof(Users)); // Redirigir al listado de usuarios
            }
            return View(userDto); // Volver a mostrar el formulario si hay errores
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUser(id); // Llamar al servicio para eliminar el usuario
            return RedirectToAction(nameof(Users)); // Redirigir al listado de usuarios
        }
    }
}
