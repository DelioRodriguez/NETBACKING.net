using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.SingleTransfer;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.SingleTransfer;

namespace NETBACKING.CORE.APPLICATION.Services.SingleTransfer
{
    public class SingleTransferService : ISingleTransferService
    {
        private readonly ISingleTransferRepository _singleTransferRepository;

        public SingleTransferService(ISingleTransferRepository singleTransferRepository)
        {
            _singleTransferRepository = singleTransferRepository;
        }

        public async Task<bool> ExecuteTransferAsync(int sourceAccountId, int destinationAccountId, decimal amount)
        {
            return await _singleTransferRepository.TransferAsync(sourceAccountId, destinationAccountId, amount);
        }
    }
}
