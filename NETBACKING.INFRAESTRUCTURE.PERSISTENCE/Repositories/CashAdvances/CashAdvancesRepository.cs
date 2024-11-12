using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.CashAvance;
using NETBACKING.CORE.DOMAIN.Entities;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Generic;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.CashAdvances
{
    public class CashAdvancesRepository : Repository<CashAdvance>, ICashAdvancesRepository
    {
       
        public CashAdvancesRepository(AppDbContext context )  : base(context)
        {
            
        }

    }
}
