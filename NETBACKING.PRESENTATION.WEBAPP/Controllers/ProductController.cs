using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Enums;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.ViewModels.Products;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers;
[Authorize(Roles = nameof(Roles.Admin))]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
   
    public async Task<IActionResult> Index(string userId)
    {
        var products = await _productService.GetAllProductsByModel(userId);
        ViewBag.UserId = userId;
        return View(products);
    }

   
    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductCreateViewModel productViewModel, string userId)
    {

        productViewModel.IsPrimary = false;
        productViewModel.ApplicationUserId = userId;
        await _productService.CreateProduct(productViewModel);
        return RedirectToAction("Index", new { userId });
    }
    
  
    [HttpPost]
    public async Task<IActionResult> DeleteProduct(string id, string productType, string userId)
        {
        var product = await _productService.GetProductByIdentificador(id);

        if (product == null)
        {
            return NotFound();
        }

        switch (productType.ToLower())
        {
            case "cuentaahorro":
                if (product.IsPrimary)
                {
                    ModelState.AddModelError("", "No se puede eliminar la cuenta primaria.");
                    return RedirectToAction("Index", new { userId });
                }
           
                await _productService.TransferToPrimaryAccount(userId, product.Balance);
                break;

            case "prestamo":
                if (product.LoanAmount > 0)
                {
                    ModelState.AddModelError("", "El préstamo no puede eliminarse si aún tiene saldo pendiente.");
                    return RedirectToAction("Index", new { userId });
                }
                break;

            case "tarjetacredito":
              
                break;
        }

        await _productService.DeleteProduct(id);
        return RedirectToAction("Index", new { userId });
    }
    
    

    public IActionResult Deposit()
    {
        return View();
    }
   
    [HttpPost]
    public async Task<IActionResult> Deposit(DepositViewModel depositViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(depositViewModel);
        }

        var product = await _productService.GetProductByIdentificador(depositViewModel.AccountNumber);
        if (product == null || product.ProductType.ToLower() != "cuentaahorro")
        {
            ModelState.AddModelError("", "Producto no es una cuenta de ahorros.");
            return View(depositViewModel);
        }

       
        await _productService.DepositToAccount(product.UniqueIdentifier, depositViewModel.Amount);
        
        return RedirectToAction(controllerName:"Admin",actionName:"Index");
    }
    
}




