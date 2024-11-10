using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Interfaces.Services;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Express;
using NETBACKING.CORE.APPLICATION.ViewModels.Payments.Express;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers;

[Authorize(Roles = nameof(Roles.Client))]
public class ExpressController : Controller
{
    private readonly IProductService _service;
    private readonly IExpressService _expressService;
    private readonly IUserService _userService;

    public ExpressController(IProductService service, IExpressService expressService, IUserService userService)
    {
        this._service = service;
        _expressService = expressService;
        _userService = userService;
    }

    public async Task<IActionResult> IndexExpress()
    {
        return View(await _service.GetProductsBycurrentCard(User.FindFirstValue(ClaimTypes.NameIdentifier)) );
    }

    [HttpPost]
    public async Task<IActionResult> IndexExpress(string accountNumber, decimal paymentAmount, string originAccount)
    {
        try
        {
            var model = new ExpressViewModel
            {
                AccountNumber = accountNumber,
                PaymentAmount = paymentAmount,
                OriginAccount = originAccount
            };
        
            if (accountNumber.Length != 9 || paymentAmount <= 0 || originAccount.Length != 9)
            {
                TempData["ErrorMessage"] = "Todos los campos son requeridos.";
                return RedirectToAction("IndexExpress");  
            }
        
            await _expressService.RealizarPagoExpressAsync(model);
            TempData["SuccessMessage"] = "Pago realizado con exito.";
            return RedirectToAction("IndexExpress");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("IndexExpress");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAccountOwnerName(string accountNumber)
    {
        try
        {
            var ownerName = await _service.GetProductByIdentificador(accountNumber);
            
            if (ownerName != null)
            {
                var owner = await _userService.GetUserById(ownerName.ApplicationUserId);
                return Json(new { success = true, ownerName = owner.FirstName + " " + owner.LastName  });
            }
            
            return Json(new { success = false });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }
}

