using FluentAssertions;
using itabank.SDK.Consumers.Interfaces;
using itabank.SDK.Extensions;
using itabank.SDK.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace itabank.SDK.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Test]
    public void OK()
    {
        var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string> {
            { "ApiSettings:BaseUrl", "http://test.url" }
        }).Build();

        var services = new ServiceCollection();

        services.AddItaBankSDK(config);

        var provider = services.BuildServiceProvider();

        provider.GetRequiredService<IApiConsumer>().Should().NotBeNull();
        provider.GetRequiredService<IOptions<ApiSettings>>().Should().NotBeNull();
    }
}