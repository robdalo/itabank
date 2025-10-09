using FluentAssertions;
using itabank.SDK.Consumers.Interfaces;
using itabank.SDK.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Internal;

namespace itabank.SDK.Tests;

[Ignore("Integration")]
public class AccountTests
{
    private IApiConsumer _apiConsumer;

    private const string BaseUrl = "https://itabank-dev-webapi-g2f2bmbfbccudzbm.uksouth-01.azurewebsites.net";

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var provider = GetServiceProvider();
        _apiConsumer = provider.GetRequiredService<IApiConsumer>();
    }

    [Test]
    public async Task GetAccountByNumber()
    {
        var accountNumber = "000001";

        var account = await _apiConsumer.GetAccountByNumberAsync(accountNumber);

        account.Should().NotBeNull();
    }

    private IServiceProvider GetServiceProvider()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> {
                { "ApiSettings:BaseUrl", BaseUrl }})
            .Build();

        var services = new ServiceCollection();

        services.AddItaBankSDK(config);

        return services.BuildServiceProvider();
    }
}