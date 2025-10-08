using itabank.Core.Mapping.Profiles;
using itabank.Core.Repositories;
using itabank.Core.Repositories.Interfaces;
using itabank.Core.Services;
using itabank.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace itabank.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddItaBank(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => {
            cfg.AddProfile<DomainToSDK>();
            cfg.AddProfile<SDKToDomain>();
            cfg.LicenseKey = "";
        });

        services.AddLogging();
                
        services.AddSingleton<IAccountRepo, AccountRepo>();
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<ITransactionService, TransactionService>();

        return services;
    }
}