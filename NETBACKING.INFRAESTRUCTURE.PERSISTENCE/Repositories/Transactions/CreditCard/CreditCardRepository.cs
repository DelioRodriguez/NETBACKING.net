using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Transactions.CreditCard;
using NETBACKING.CORE.DOMAIN.Entities;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Generic;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Transactions.CreditCard;

public class CreditCardRepository : Repository<Transaction>, ICreditCardRepository
{
    public CreditCardRepository(AppDbContext context) : base(context)
    {
    }
}