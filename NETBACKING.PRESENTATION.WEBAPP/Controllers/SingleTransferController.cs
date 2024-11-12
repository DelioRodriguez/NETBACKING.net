using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.SingleTransfer;
using NETBACKING.CORE.APPLICATION.ViewModels.SingleTransfer;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NETBACKING.CORE.APPLICATION.Enums;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers
{
    [Authorize(Roles = nameof(Roles.Client))]
    public class SingleTransferController : Controller
    {
        private readonly ISingleTransferService _singleTransferService;
        private readonly IProductService _productService;

        public SingleTransferController(ISingleTransferService singleTransferService, IProductService productService)
        {
            _singleTransferService = singleTransferService;
            _productService = productService;
        }

        public async Task<IActionResult> CreateSingleTransfer()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var accounts = await _productService.GetProductsBycurrentCard(userId);
            var model = new TransferViewModel
            {
                Accounts = accounts.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSingleTransfer(TransferViewModel model)
        {
            if(model.DestinationAccountId == -1 || model.SourceAccountId == -1 || !ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId)) return Unauthorized();

                model.Accounts = (await _productService.GetProductsBycurrentCard(userId)).ToList();

                return View(model);
            }

            try
            {
                var success = await _singleTransferService.ExecuteTransferAsync(
                    model.SourceAccountId,
                    model.DestinationAccountId,
                    model.Amount);

                if (success)
                    TempData["SuccessMessage"] = "Transferencia realizada con exito.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("CreateSingleTransfer");
            }

            return RedirectToAction("CreateSingleTransfer");
        }
    }
}