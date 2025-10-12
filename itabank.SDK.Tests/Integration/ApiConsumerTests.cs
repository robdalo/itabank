using FluentAssertions;
using itabank.SDK.Consumers.Interfaces;
using itabank.SDK.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Internal;

namespace itabank.SDK.Tests;

[Ignore("Integration")]
public class ApiConsumerTests
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
    public async Task Setup()
    {
        await _apiConsumer.TruncateAsync();
    }

    [Test]
    public async Task AddOrUpdateAsync()
    {
        // add account

        var expected = new
        {
            Number = "000001",
            Name = "Mr Ted Crilly",
            Balance = (decimal)(0)
        };

        var account = await _apiConsumer.AddOrUpdateAccountAsync(new()
        {
            Name = expected.Name
        });

        account.Number.Should().Be(expected.Number);
        account.Name.Should().Be(expected.Name);
        account.Balance.Should().Be(expected.Balance);

        // update account

        account.Balance = 25;

        await _apiConsumer.AddOrUpdateAccountAsync(new()
        {
            Name = expected.Name,
            Number = expected.Number,
            Balance = 25
        });

        // get account

        account = await _apiConsumer.GetAccountAsync(account.Number);

        account.Number.Should().Be(expected.Number);
        account.Name.Should().Be(expected.Name);
        account.Balance.Should().Be(25);
    }

    [Test]
    public async Task GetAccountsAsync()
    {
        // add accounts

        var expected = new[] {
            new {
                Id = 1,
                Number = "000001",
                Name = "Mr Ted Crilly",
                Balance = (decimal)(25)
            },
            new {
                Id = 2,
                Number = "000002",
                Name = "Mr Fred Olsen",
                Balance = (decimal)(50)
            }
        };

        await _apiConsumer.AddOrUpdateAccountAsync(new()
        {
            Name = expected[0].Name,
            Balance = expected[0].Balance
        });

        await _apiConsumer.AddOrUpdateAccountAsync(new()
        {
            Name = expected[1].Name,
            Balance = expected[1].Balance
        });

        // get accounts

        var accounts = await _apiConsumer.GetAccountsAsync();

        accounts.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public async Task GetAccountByIdAsync()
    {
        // add account

        var expected = new {
            Id = 1,
            Number = "000001",
            Name = "Mr Ted Crilly",
            Balance = (decimal)(0)
        };

        await _apiConsumer.AddOrUpdateAccountAsync(new()
        {
            Name = expected.Name
        });
        
        // get account

        var account = await _apiConsumer.GetAccountAsync(expected.Id);

        account.Number.Should().Be(expected.Number);
        account.Name.Should().Be(expected.Name);
        account.Balance.Should().Be(expected.Balance);
    }

    [Test]
    public async Task GetAccountByNumberAsync()
    {
        // add account

        var expected = new {
            Number = "000001",
            Name = "Mr Ted Crilly",
            Balance = (decimal)(0)
        };

        await _apiConsumer.AddOrUpdateAccountAsync(new()
        {
            Name = expected.Name
        });

        // get account
        
        var account = await _apiConsumer.GetAccountAsync(expected.Number);

        account.Number.Should().Be(expected.Number);
        account.Name.Should().Be(expected.Name);
        account.Balance.Should().Be(expected.Balance);
    }

    [Test]
    public async Task PostTransactionAsync()
    {
        // add accounts

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

        // get accounts

        var account = await _apiConsumer.GetAccountAsync(payerAccount.Number);

        account.Number.Should().Be(payerAccount.Number);
        account.Name.Should().Be(payerAccount.Name);
        account.Balance.Should().Be(payerAccount.Balance);

        account = await _apiConsumer.GetAccountAsync(payeeAccount.Number);

        account.Number.Should().Be(payeeAccount.Number);
        account.Name.Should().Be(payeeAccount.Name);
        account.Balance.Should().Be(payeeAccount.Balance);

        // post transaction

        await _apiConsumer.PostTransactionAsync(new()
        {
            Payer = payerAccount.Number,
            Payee = payeeAccount.Number,
            Value = 50
        });

        // get accounts

        account = await _apiConsumer.GetAccountAsync(payerAccount.Number);

        account.Number.Should().Be(payerAccount.Number);
        account.Name.Should().Be(payerAccount.Name);
        account.Balance.Should().Be(75);

        account = await _apiConsumer.GetAccountAsync(payeeAccount.Number);

        account.Number.Should().Be(payeeAccount.Number);
        account.Name.Should().Be(payeeAccount.Name);
        account.Balance.Should().Be(50);
    }
    
    public async Task TruncateAsync()
    {
        // add account

        var expected = new {
            Id = 1,
            Number = "000001",
            Name = "Mr Ted Crilly",
            Balance = (decimal)(0)
        };

        await _apiConsumer.AddOrUpdateAccountAsync(new()
        {
            Name = expected.Name
        });

        // get accounts

        var accounts = await _apiConsumer.GetAccountsAsync();

        accounts.Any().Should().BeTrue();

        // truncate

        await _apiConsumer.TruncateAsync();

        accounts = await _apiConsumer.GetAccountsAsync();

        accounts.Any().Should().BeFalse();
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