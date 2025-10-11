using itabank.Core.Mapping.Profiles;
using itabank.Core.Repositories;
using itabank.Core.Repositories.Interfaces;
using itabank.Core.Services;
using itabank.Core.Services.Interfaces;
using itabank.Core.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace itabank.Core.Extensions;

public static class ServiceCollectionExtensions
{
    private const string DatabaseName = "itabank.db";

    public static IServiceCollection AddItaBank(this IServiceCollection services)
    {
        services.Configure<DatabaseSettings>(x => {
            x.Name = IsRunningInAzure() ?
                Path.Combine(Path.GetTempPath(), DatabaseName) : DatabaseName;          
        });
        
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
    
    private static bool IsRunningInAzure()
    {
        return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME"));
    }
}