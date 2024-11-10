using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.CashAdvances;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.ViewModels.CashAdvance;
using System.Security.Claims;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers
{
    public class CashAdvancesController : Controller
    {
        private readonly ICashAdvancesService _cashAdvanceService;
        private readonly IProductService _productService;

        public CashAdvancesController(ICashAdvancesService cashAdvanceService, IProductService productService)
        {
            _cashAdvanceService = cashAdvanceService;
            _productService = productService;
        }

        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var creditCards = await _productService.GetProductsByCreditCard(userId);
            var savingsAccounts = await _productService.GetProductsBycurrentCard(userId);

            if (creditCards == null || savingsAccounts == null)
            {
                TempData["ErrorMessage"] = "No se cargo ni las tarjetas ni las cuentas.";
                return RedirectToAction("Create");
            }

            var model = new CashAdvanceViewModel
            {
                CreditCards = creditCards.ToList(),
                SavingsAccounts = savingsAccounts.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CashAdvanceViewModel model)
        {
            if (model.CreditCardId == -1 || model.DestinationAccountId == -1)
            {
                ModelState.AddModelError("", "Por favor, seleccione una tarjeta de credito y una cuenta destino valida.");
            }

            if (!ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.CreditCards = (await _productService.GetProductsByCreditCard(userId)).ToList();
                model.SavingsAccounts = (await _productService.GetProductsBycurrentCard(userId)).ToList();
                return View(model);
            }

            try
            {
                var success = await _cashAdvanceService.ProcessCashAdvance(
                    model.CreditCardId,
                    model.DestinationAccountId,
                    model.Amount);

                if (success)
                    TempData["SuccessMessage"] = "El avance de efectivo fue realizado con exito.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Create");
            }

            return RedirectToAction("Create");
        }
    }
}
