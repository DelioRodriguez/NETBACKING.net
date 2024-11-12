using System.Transactions;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Transactions.Express;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Generic;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Transactions.Express;

public class ExpressRepository : Repository<Transaction>, IExpressRepository
{
    
    
    public ExpressRepository(AppDbContext context) : base(context)
    {
      
    }
}