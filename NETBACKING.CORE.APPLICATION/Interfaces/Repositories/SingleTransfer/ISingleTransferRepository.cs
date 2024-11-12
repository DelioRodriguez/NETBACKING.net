
namespace NETBACKING.CORE.APPLICATION.Interfaces.Repositories.SingleTransfer
{
    public interface ISingleTransferRepository
    {
        Task<bool> TransferAsync(int sourceAccountId, int destinationAccountId, decimal amount);
    }
}
