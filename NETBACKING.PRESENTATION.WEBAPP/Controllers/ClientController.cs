using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers;

[Authorize(Roles = nameof(Roles.Client))]
public class ClientController : Controller
{
    private readonly IProductService _productService;

    public ClientController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId != null)
        {
            var products = await _productService.GetAllProductsByModel(userId);
            return View(products);
        }

        return Unauthorized();
    }
}