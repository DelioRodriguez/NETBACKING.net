using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Loan;
using NETBACKING.CORE.APPLICATION.ViewModels.Payments.Loan;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers;

[Authorize(Roles = nameof(Roles.Client))]
public class LoanController : Controller
{
    private readonly IProductService _productService;
    private readonly ILoanService _loanService;

    public LoanController(IProductService productService, ILoanService loanService)
    {
        _productService = productService;
        _loanService = loanService;
    }

    public async Task<IActionResult> IndexLoan()
    {
        var l = await _productService.GetProductsByLoan(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var c = await _productService.GetProductsBycurrentCard(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var loans = new CombinedLoanViewModel
        {
            Loans = l.ToList(),
            Current =  c.ToList()
        };
        
        return View(loans);
    }
    
    [HttpPost]
    public async Task<IActionResult> IndexLoan(string loanId, string accountId, decimal amount)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Datos invalidos. Por favor revisa la informacion.";
                return RedirectToAction("IndexLoan");
            }

            var paymentSuccess = await _loanService.PayLoanCardAsync(loanId, accountId, amount);

            if (paymentSuccess)
            {
                TempData["SuccessMessage"] = "Pago realizado con exito.";
                return RedirectToAction("indexLoan");
            }
            
                TempData["ErrorMessage"] = "No se pudo procesar el pago. Verifica los datos y vuelve a intentarlo.";
                return RedirectToAction("indexLoan");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("indexLoan");
        }
    }
}