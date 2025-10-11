using FluentAssertions;
using itabank.SDK.Consumers.Interfaces;
using itabank.SDK.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Internal;

namespace itabank.SDK.Tests;

// [Ignore("Integration")]
public class AccountTests
{
    private IApiConsumer _apiConsumer;

    private const string BaseUrl = "http://localhost:5263";

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var provider = GetServiceProvider();
        _apiConsumer = provider.GetRequiredService<IApiConsumer>();
    }

    [SetUp]
    public async Task Setup()
    {
        await _apiConsumer.TruncateAsync();
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
        var number = "000001";
        var name = "Mr Ted Crilly";
        var balance = (decimal)(0);

        await _apiConsumer.AddOrUpdateAccountAsync(new()
        {
            Name = name
        });

        var account = await _apiConsumer.GetAccountAsync(number);

        account.Number.Should().Be(number);
        account.Name.Should().Be(name);
        account.Balance.Should().Be(balance);
    }
    
    [Test]
    public async Task PostTransaction()
    {
        var payerAccount = new
        {
            Number = "000001",
            Name = "Mr Ted Crilly",
            Balance = (decimal)(125)
        };

        var payeeAccount = new
        {
            Number = "000002",
            Name = "Mr Fred Olsen",
            Balance = (decimal)(0)            
        };

        await _apiConsumer.AddOrUpdateAccountAsync(new()
        {
            Name = payerAccount.Name,
            Balance = payerAccount.Balance
        });
        
        await _apiConsumer.AddOrUpdateAccountAsync(new() 
        {
            Name = payeeAccount.Name,
            Balance = payeeAccount.Balance
        });

        var account = await _apiConsumer.GetAccountAsync(payerAccount.Number);

        account.Number.Should().Be(payerAccount.Number);
        account.Name.Should().Be(payerAccount.Name);
        account.Balance.Should().Be(payerAccount.Balance);

        account = await _apiConsumer.GetAccountAsync(payeeAccount.Number);

        account.Number.Should().Be(payeeAccount.Number);
        account.Name.Should().Be(payeeAccount.Name);
        account.Balance.Should().Be(payeeAccount.Balance);

        await _apiConsumer.PostTransactionAsync(new()
        {
            PayerAccountNumber = payerAccount.Number,
            PayeeAccountNumber = payeeAccount.Number,
            Value = 50
        });

        account = await _apiConsumer.GetAccountAsync(payerAccount.Number);

        account.Number.Should().Be(payerAccount.Number);
        account.Name.Should().Be(payerAccount.Name);
        account.Balance.Should().Be(75);

        account = await _apiConsumer.GetAccountAsync(payeeAccount.Number);

        account.Number.Should().Be(payeeAccount.Number);
        account.Name.Should().Be(payeeAccount.Name);
        account.Balance.Should().Be(50);
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