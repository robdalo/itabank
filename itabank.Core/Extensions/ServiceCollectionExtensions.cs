using itabank.Core.Repositories;
using itabank.Core.Repositories.Interfaces;
using itabank.Core.Services;
using itabank.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace itapoker.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddItaBank(this IServiceCollection services)
    {
        services.AddSingleton<IAccountRepo, AccountRepo>();

        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<ITransactionService, TransactionService>();

        return services;
    }
}