using NETBACKING.CORE.APPLICATION.Interfaces.Repositories;
using NETBACKING.CORE.APPLICATION.Interfaces.Services;
using NETBACKING.CORE.APPLICATION.ViewModels;

namespace NETBACKING.CORE.APPLICATION.Services;

public class DashBoardService : IDashBoardService
{
    private readonly IDashBoardRepository _dashBoardRepository;

    public DashBoardService(IDashBoardRepository dashBoardRepository)
    {
        _dashBoardRepository = dashBoardRepository;
    }
    public DashboardViewModel GetDashboardData()
    {
        return new DashboardViewModel()
        {
            TotalTransactions = _dashBoardRepository.GetTotalTransactions(),
            TransactionsToday = _dashBoardRepository.GetTransactionsToday(),
            TotalPayments = _dashBoardRepository.GetTotalPayments(),
            PaymentsToday = _dashBoardRepository.GetPaymentsToday(),
            ActiveCustomers = _dashBoardRepository.GetActiveCustomers(),
            InactiveCustomers = _dashBoardRepository.GetInactiveCustomers(),
            AssignedProducts = _dashBoardRepository.GetAssignedProducts()
        };
    }
}