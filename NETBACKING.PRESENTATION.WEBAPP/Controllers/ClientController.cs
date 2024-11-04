using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Enums;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers;
[Authorize(Roles = nameof(Roles.Client))]
public class ClientController : Controller
{
    public IActionResult Index()
    {
        throw new NotImplementedException();
    }
}