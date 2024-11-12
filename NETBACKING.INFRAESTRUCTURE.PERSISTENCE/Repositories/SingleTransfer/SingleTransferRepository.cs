using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.SingleTransfer;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.SingleTransfer
{
    public class SingleTransferRepository : ISingleTransferRepository
    {
        private readonly IProductRepository _productRepository;

        public SingleTransferRepository(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> TransferAsync(int sourceAccountId, int destinationAccountId, decimal amount)
        {
            var sourceAccount = await _productRepository.GetByIdAsync(sourceAccountId);
            var destinationAccount = await _productRepository.GetByIdAsync(destinationAccountId);

            if (sourceAccount == null || destinationAccount == null)
                throw new Exception("Cuenta de origen o destino no encontrada.");

            if (sourceAccount.Balance < amount)
                throw new Exception("Saldo insuficiente en la cuenta de origen.");

            sourceAccount.Balance -= amount;
            destinationAccount.Balance += amount;

            await _productRepository.UpdateAsync(sourceAccount);
            await _productRepository.UpdateAsync(destinationAccount);

            return true;
        }
    }
}
