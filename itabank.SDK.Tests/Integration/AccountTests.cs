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

    [SetUp]
    public void Setup()
    {
        _apiConsumer.TruncateAsync();
    }

    [Test]
    public async Task AddOrUpdateAsync()
    {
        var number = "000001";
        var name = "Mr Ted Crilly";
        var balance = (decimal)(0);

        var account = await _apiConsumer.AddOrUpdateAccountAsync(new() {
            Name = name
        });

        account.Number.Should().Be(number);
        account.Name.Should().Be(name);
        account.Balance.Should().Be(balance);
    }

    [Test]
    public async Task GetAccountByNumberAsync()
    {
        var accountNumber = "000001";

        var account = await _apiConsumer.GetAccountAsync(accountNumber);

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