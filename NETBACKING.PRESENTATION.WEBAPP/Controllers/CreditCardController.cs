using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.CreditCard;
using NETBACKING.CORE.APPLICATION.ViewModels.Payments.CreditCard;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers;

[Authorize(Roles = nameof(Roles.Client))]
public class CreditCardController : Controller
{
    private readonly IProductService _productService;
    private readonly ICreditCardService _creditCardService;

    public CreditCardController(IProductService productService, ICreditCardService creditCardService)
    {
        _productService = productService;
        _creditCardService = creditCardService;
    }

    public async Task<IActionResult> IndexCreditCard()
    {
        var creditCard = await _productService.GetProductsByCreditCard(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var currentcard = await _productService.GetProductsBycurrentCard(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var tarjetas = new CombinedCardViewModel
        {
            Current = currentcard.ToList(),
            CreditCards = creditCard.ToList()
        };

        if (!ModelState.IsValid)
        {
            return View(tarjetas);
        }
        
        return View(tarjetas);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> IndexCreditCard(string creditCard, string originAccount, decimal paymentAmount)
    {
        try
        {
            var creditExiste = await _productService.GetProductByIdentificador(creditCard);
            
            if (creditExiste!.Balance == 0)
            {
                throw new ApplicationException("La deuda ya esta pagada.");
            }
            
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                TempData["ErrorMessage"] = "Datos invalidos. Por favor revisa la informacion.";
                return RedirectToAction("IndexCreditCard");
            }

            var paymentSuccess = await _creditCardService.PayCreditCardAsync(creditCard, originAccount, paymentAmount);

            if (paymentSuccess)
            {
                TempData["SuccessMessage"] = "Pago realizado con exito.";
                return RedirectToAction("IndexCreditCard");
            }
            else
            {
                TempData["ErrorMessage"] = "No se pudo procesar el pago. Verifica los datos y vuelve a intentarlo.";
                return RedirectToAction("IndexCreditCard");
            }
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("IndexCreditCard");
        }
    }
}