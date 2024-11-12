using Microsoft.AspNetCore.Mvc;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Transactions;
using NETBACKING.CORE.APPLICATION.ViewModels.Transactions;
using System.Security.Claims;

namespace NETBACKING.PRESENTATION.WEBAPP.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionsService _transactionService;

        public TransactionController(ITransactionsService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("AccountId no encontrado.");

            var transactions = await _transactionService.GetTransactionsByAccountAsync(userId);

            return View(transactions);
        }
    }
}
