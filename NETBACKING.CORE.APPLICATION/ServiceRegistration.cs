using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NETBACKING.CORE.APPLICATION.Interfaces.Services;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Beneficiarys;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.CashAdvances;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Generic;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.SingleTransfer;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.CreditCard;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Express;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Loan;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Transactions;
using NETBACKING.CORE.APPLICATION.Mappers;
using NETBACKING.CORE.APPLICATION.Services;
using NETBACKING.CORE.APPLICATION.Services.Beneficiar;
using NETBACKING.CORE.APPLICATION.Services.CashAvances;
using NETBACKING.CORE.APPLICATION.Services.Generic;
using NETBACKING.CORE.APPLICATION.Services.Products;
using NETBACKING.CORE.APPLICATION.Services.SingleTransfer;
using NETBACKING.CORE.APPLICATION.Services.Transactions.CreditCard;
using NETBACKING.CORE.APPLICATION.Services.Transactions.Express;
using NETBACKING.CORE.APPLICATION.Services.Transactions.Loan;
using NETBACKING.CORE.APPLICATION.Services.Transactions.Transactions;

namespace NETBACKING.CORE.APPLICATION
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService,AccountService>();
            services.AddScoped<IBeneficiaryService,BeneficiaryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IExpressService, ExpressService>();
            services.AddScoped<ICreditCardService, CreditCardService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<ICashAdvancesService, CashAdvancesService>();
            services.AddScoped<ISingleTransferService, SingleTransferService>();
            services.AddScoped<ITransactionsService, TransactionsService>();
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(GeneralProfile));
           
            services.AddScoped<IDashBoardService, DashBoardService>();
        }
    }
}
