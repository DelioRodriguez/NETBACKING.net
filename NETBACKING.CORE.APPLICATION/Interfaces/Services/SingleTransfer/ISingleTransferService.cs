

namespace NETBACKING.CORE.APPLICATION.Interfaces.Services.SingleTransfer
{
    public interface ISingleTransferService 
    {
        Task<bool> ExecuteTransferAsync(int sourceAccountId, int destinationAccountId, decimal amount);
    }
}
