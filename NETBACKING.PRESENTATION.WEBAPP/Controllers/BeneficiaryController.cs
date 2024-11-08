using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Exceptions;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Beneficiarys;
using NETBACKING.CORE.APPLICATION.ViewModels.Beneficiary;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers;

public class BeneficiaryController : Controller
{
    private readonly IBeneficiaryService _beneficiaryService;

    public BeneficiaryController(IBeneficiaryService beneficiaryService)
    {
        _beneficiaryService = beneficiaryService;
    }

    public async Task<IActionResult> Beneficiaries()
    {
        var u = await _beneficiaryService.GetByIdUserAsyncModel(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return View(u);
    }

    [HttpPost]
    public async Task<IActionResult> Beneficiaries(string idCuenta)
    {
        if (string.IsNullOrEmpty(idCuenta))
        {
            TempData["ErrorMessage"] = "El campo 'idCuenta' no puede estar vacío.";
            return RedirectToAction("Beneficiaries");
        }
        
        if (idCuenta.Length != 9)
        {
            TempData["ErrorMessage"] = "El número de cuenta debe tener exactamente 9 dígitos.";
            return RedirectToAction("Beneficiaries");
        }
        
        try
        {
            await _beneficiaryService.AddAsyncByModel(idCuenta);
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