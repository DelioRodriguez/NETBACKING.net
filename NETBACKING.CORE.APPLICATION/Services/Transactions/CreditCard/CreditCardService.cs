using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.CreditCard;
using NETBACKING.CORE.APPLICATION.Services.Generic;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Services.Transactions.CreditCard;

public class CreditCardService : Service<Transaction>, ICreditCardService
{
    private readonly IProductRepository _productRepository;
    
    public CreditCardService(IRepository<Transaction> repository, IProductRepository productRepository) : base(repository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> PayCreditCardAsync(string creditCard, string originAccount, decimal paymentAmount)
    {
        var credit = await _productRepository.GetProductByIdentificador(creditCard);
        var original = await _productRepository.GetProductByIdentificador(originAccount);

      
        var transaction = new Transaction
        {
            Date = DateTime.Now,
            TransactionType = "Tarjeta de Credito",
            Amount = paymentAmount,
            SourceAccountId = original.Id,
            DestinationAccountId = credit.Id,
            SourceAccount = original,
            DestinationAccount = credit
        };
        
        if (paymentAmount > credit.Balance)
        {
            decimal? restante = paymentAmount - credit.Balance;
                
            credit.Balance = 0;
                
            original.Balance -= paymentAmount;
                
            original.Balance += restante;
        }
        else
        {
            original.Balance -= paymentAmount;
            credit.Balance -= paymentAmount;
        }

        await _productRepository.UpdateAsync(original);
        await _productRepository.UpdateAsync(credit);
        await AddAsync(transaction);

        return true;
    }
}