using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.CashAvance;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.CashAdvances;
using NETBACKING.CORE.APPLICATION.Services.Generic;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Services.CashAvances
{
    public class CashAdvancesService : Service<CashAdvance>, ICashAdvancesService
    {
        public readonly ICashAdvancesRepository _cashAdvancesRepository;
        private readonly IProductRepository _productRepository;
        public CashAdvancesService(IRepository<CashAdvance> repository, ICashAdvancesRepository cashAdvancesRepository, IProductRepository productRepository) : base(repository)
        {
            _productRepository = productRepository;
            _cashAdvancesRepository = cashAdvancesRepository;
        }

        public async Task<bool> ProcessCashAdvance(int creditCardId, int destinationAccountId, decimal amount)
        {
            var creditCard = await _productRepository.GetByIdAsync(creditCardId);
            var destinationAccount = await _productRepository.GetByIdAsync(destinationAccountId);

            if (creditCard == null || destinationAccount == null)
                throw new Exception("Tarjeta de credito o cuenta de destino no encontrada.");

            var interest = amount * 0.0625M;

            var cashAdvance = new CashAdvance
            {
                CreditCardId = creditCardId,
                DestinationAccountId = destinationAccountId,
                Amount = amount,
                Interest = interest,
                Date = DateTime.Now
            };

            creditCard.Balance += amount + interest;
            destinationAccount.Balance += amount;

            if (creditCard.Balance <= creditCard.CreditLimit){

                await _cashAdvancesRepository.AddAsync(cashAdvance);
                await _productRepository.UpdateAsync(creditCard);
                await _productRepository.UpdateAsync(destinationAccount);
            }
            else
            {
                throw new Exception("El monto solicitado excede el limite de credito de la tarjeta seleccionada.");
            }

            return true;
        }
    }
}
