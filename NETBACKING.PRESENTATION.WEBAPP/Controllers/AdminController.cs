using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Interfaces.Services;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class AdminController : Controller
    {
        private readonly IDashBoardService _dashboardService;

        public AdminController(IDashBoardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            var dashboardData = _dashboardService.GetDashboardData();
            return View(dashboardData);
        }
    }
}
