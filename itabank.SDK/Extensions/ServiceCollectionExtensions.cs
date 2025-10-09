using itabank.SDK.Consumers;
using itabank.SDK.Consumers.Interfaces;
using itabank.SDK.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace itabank.SDK.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddItaBankSDK(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<ApiSettings>()
                .Bind(config.GetSection("ApiSettings"));

        services.AddSingleton<IApiConsumer, ApiConsumer>();

        return services;
    }
}