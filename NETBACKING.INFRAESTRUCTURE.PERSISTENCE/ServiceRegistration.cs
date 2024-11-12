using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Beneficiarys;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.CashAvance;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.SingleTransfer;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Transactions.CreditCard;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Transactions.Express;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Transactions.Loan;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Transactions.Transactions;

using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;

using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Beneficiarys;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.CashAdvances;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.DashBoard;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Generic;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Products;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.SingleTransfer;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Transactions.CreditCard;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Transactions.Express;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Transactions.Loan;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Transactions.Transactions;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.User;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE
{
    public static class ServiceRegistration
    {
        public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDashBoardRepository, DashBoardRepository>();
            services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IExpressRepository, ExpressRepository>();
            services.AddScoped<ICreditCardRepository, CreditCardRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<ICashAdvancesRepository, CashAdvancesRepository>();
            services.AddScoped<ISingleTransferRepository, SingleTransferRepository>();
            services.AddScoped<ITransactionsRepository, TransactionsRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("ContextDb");
                });
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        mbox => mbox.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
                });
            }
        }
    }
}