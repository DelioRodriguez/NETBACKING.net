using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Exceptions;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Beneficiarys;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Express;
using NETBACKING.CORE.APPLICATION.ViewModels.Beneficiary;
using NETBACKING.CORE.APPLICATION.ViewModels.Payments.Express;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers;

[Authorize(Roles = nameof(Roles.Client))]
public class BeneficiaryController : Controller
{
    private readonly IBeneficiaryService _beneficiaryService;
    private readonly IProductService _productService;
    private readonly IExpressService _expressService;

    public BeneficiaryController(IBeneficiaryService beneficiaryService, IProductService productService, IExpressService expressService)
    {
        _beneficiaryService = beneficiaryService;
        _productService = productService;
        _expressService = expressService;
    }

    public async Task<IActionResult> Beneficiaries()
    {
        var u = await _beneficiaryService.GetByIdUserAsyncModel(User.FindFirstValue(ClaimTypes.NameIdentifier));

        return View(u);
    }

    public async Task<IActionResult> PagoBeneficiaries()
    {
        var beneficiarios = await _beneficiaryService.GetByIdUserAsyncModel(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var cuentas = await _productService.GetProductsBycurrentCard(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (cuentas.Count() != 0)
        {
            foreach (var beneficiario in beneficiarios)
            {
            
                beneficiario.Current = cuentas.ToList();
            }
            
            return View(beneficiarios);
        }
        
        return View(beneficiarios);
    }
    
    [HttpPost]
    public async Task<IActionResult> PagoBeneficiaries(string accountNumber, decimal paymentAmount, string originAccount)
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
                return RedirectToAction("PagoBeneficiaries");  
            }
        
            await _expressService.RealizarPagoBeneficiariosAsync(model);
            TempData["SuccessMessage"] = "Pago realizado con exito.";
            return RedirectToAction("PagoBeneficiaries");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("PagoBeneficiaries");
        }
    }


    [HttpPost]
    public async Task<IActionResult> Beneficiaries(string idCuenta)
    {
        if (string.IsNullOrEmpty(idCuenta))
        {
            TempData["ErrorMessage"] = "El campo 'idCuenta' no puede estar vacio.";
            return RedirectToAction("Beneficiaries");
        }

        var u = await _beneficiaryService.GetBeneficiaryByIdCuentaAndbyUserId(idCuenta, User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        if (u != null)
        {
            TempData["ErrorMessage"] = "El Beneficiario ya esta agregado.";
            return RedirectToAction("Beneficiaries");
        }
        
        if (idCuenta.Length != 9)
        {
            TempData["ErrorMessage"] = "El numero de cuenta debe tener exactamente 9 digitos.";
            return RedirectToAction("Beneficiaries");
        }
        
        try
        {
            await _beneficiaryService.AddAsyncByModel(idCuenta, User.FindFirstValue(ClaimTypes.NameIdentifier));
            TempData["SuccessMessage"] = "Beneficiario agregado exitosamente.";
            return RedirectToAction("Beneficiaries");
        }
        catch (AddBeneficiaryException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Beneficiaries");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Beneficiaries");
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBeneficiary(int id)
    {
        try
        {
            await _beneficiaryService.DeleteAsync(id);
            return RedirectToAction("Beneficiaries", "Beneficiary");
        }
        catch (Exception e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return View("Beneficiaries", new List<BeneficiaryViewModel>());
        }
    }
}