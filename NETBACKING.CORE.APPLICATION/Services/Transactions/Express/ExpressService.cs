using NETBACKING.CORE.APPLICATION.Exceptions.Transactions.Express;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Express;
using NETBACKING.CORE.APPLICATION.Services.Generic;
using NETBACKING.CORE.APPLICATION.ViewModels.Payments.Express;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Services.Transactions.Express;

public class ExpressService : Service<Transaction>, IExpressService
{
    private readonly IProductRepository _productRepository;
    
    public ExpressService(IRepository<Transaction> repository, IProductRepository productRepository) : base(repository)
    {
        _productRepository = productRepository;
    }

    public async Task<Transaction> RealizarPagoExpressAsync(ExpressViewModel model)
    {
        try
        {
            var sourceAccount = await _productRepository.GetProductByIdentificador(model.OriginAccount);
            var destinationAccount = await _productRepository.GetProductByIdentificador(model.AccountNumber);

            if (sourceAccount == destinationAccount)
            {
                throw new AddExpressException("No se puede realizar el pago express al mismo producto.", null);
            }

            if (sourceAccount == null || destinationAccount == null)
            {
                throw new AddExpressException("Una o ambas cuentas no existen.", null);
            }
        
            if (sourceAccount.Balance < model.PaymentAmount)
            {
                throw new AddExpressException("Saldo insuficiente en la cuenta fuente.", null);
            }
        
            var transaction = new Transaction
            {
                Date = DateTime.Now,
                TransactionType = "Pago Express",
                Amount = model.PaymentAmount,
                SourceAccountId = sourceAccount.Id,
                DestinationAccountId = destinationAccount.Id,
                SourceAccount = sourceAccount,
                DestinationAccount = destinationAccount
            };
        
            sourceAccount.Balance -= model.PaymentAmount;
            destinationAccount.Balance += model.PaymentAmount;

            await AddAsync(transaction);
            await _productRepository.UpdateAsync(sourceAccount);
            await _productRepository.UpdateAsync(destinationAccount);
        
            return transaction;
        }
        catch (Exception e)
        {
            throw new AddExpressException(e.Message, e);
        }
    }
}