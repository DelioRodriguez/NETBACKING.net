using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Loan;
using NETBACKING.CORE.APPLICATION.Services.Generic;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Services.Transactions.Loan;

public class LoanService : Service<Transaction>, ILoanService
{
    private readonly IProductService _productService;
    private readonly IProductRepository _productRepository;
    
    public LoanService(IRepository<Transaction> repository, IProductService productService, IProductRepository productRepository) : base(repository)
    {
        _productService = productService;
        _productRepository = productRepository;
    }

    public async Task<bool> PayLoanCardAsync(string loanAccount, string originAccount, decimal paymentAmount)
    {
        var credit = await _productRepository.GetProductByIdentificador(loanAccount);
        var original = await _productRepository.GetProductByIdentificador(originAccount);

        if (original!.Balance <= paymentAmount)
        {
            return false;
        }

        var transaction = new Transaction
        {
            Date = DateTime.Now,
            TransactionType = "Pago Prestamo",
            Amount = paymentAmount,
            SourceAccountId = original.Id,
            DestinationAccountId = credit?.Id,
            SourceAccount = original,
            DestinationAccount = credit
        };
        
        if (credit != null)
        {
            if (paymentAmount > credit.LoanAmount)
            {
                decimal? restante = paymentAmount - credit.LoanAmount;
                
                credit.LoanAmount = 0;
                
                original.Balance -= paymentAmount;
                
                original.Balance += restante;
            }
            else
            {
                original.Balance -= paymentAmount;
                credit.LoanAmount -= paymentAmount;
            }
            

            await _productRepository.UpdateAsync(original);
            await _productRepository.UpdateAsync(credit);
        }

        await AddAsync(transaction);

        return true;
    }
}