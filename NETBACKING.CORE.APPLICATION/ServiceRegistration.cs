using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NETBACKING.CORE.APPLICATION.Interfaces.Services;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Generic;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.Mappers;
using NETBACKING.CORE.APPLICATION.Services;
using NETBACKING.CORE.APPLICATION.Services.Generic;
using NETBACKING.CORE.APPLICATION.Services.Products;

namespace NETBACKING.CORE.APPLICATION
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService,AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(GeneralProfile));
           
            services.AddScoped<IDashBoardService, DashBoardService>();
        }
    }
}
