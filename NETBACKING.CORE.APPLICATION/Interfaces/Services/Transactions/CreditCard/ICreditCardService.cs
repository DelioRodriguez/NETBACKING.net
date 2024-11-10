using NETBACKING.CORE.APPLICATION.Interfaces.Services.Generic;
using NETBACKING.CORE.APPLICATION.ViewModels.Payments.CreditCard;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.CreditCard;

public interface ICreditCardService : IService<Transaction>
{
    Task<bool> PayCreditCardAsync(string creditCard, string originAccount, decimal paymentAmount);
}