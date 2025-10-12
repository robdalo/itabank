using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using itabank.Core.Settings;
using Microsoft.Extensions.Options;

namespace itabank.Core.Repositories.Tests;

public class AccountRepoTests
{
    private IFixture _autoFixture;

    private AccountRepo _accountRepo;

    private const string DatabaseName = "itabank.db";

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _autoFixture = new Fixture();
        _autoFixture.Customize(new AutoMoqCustomization());

        _autoFixture.Inject(Options.Create<DatabaseSettings>(new() {
            Name = DatabaseName
        }));

        _accountRepo = _autoFixture.Create<AccountRepo>();
    }

    [SetUp]
    public void Setup()
    {
        _accountRepo.Truncate();
    }

    [Test]
    public void AddOrUpdate()
    {
        var expected = new[] {
            new {
                Id = 1,
                Number = "000001",
                Name = "Mr Ted Crilly",
                Balance = 25,
            },
            new
            {
                Id = 1,
                Number = "000001",
                Name = "Mr Jack Hackett",
                Balance = 125
            },
            new
            {
                Id = 2,
                Number = "000002",
                Name = "Mr Larry Duff",
                Balance = 50
            }
        };

        // add

        var account = _accountRepo.AddOrUpdate(new()
        {
            Name = expected[0].Name,
            Balance = expected[0].Balance
        });

        account.Should().BeEquivalentTo(expected[0]);

        // update

        account.Name = expected[1].Name;
        account.Balance = expected[1].Balance;
        
        account = _accountRepo.AddOrUpdate(account);

        account.Should().BeEquivalentTo(expected[1]);

        // add

        account = _accountRepo.AddOrUpdate(new()
        {
            Name = expected[2].Name,
            Balance = expected[2].Balance
        });

        account.Should().BeEquivalentTo(expected[2]);
    }

    [Test]
    public void Get()
    {
        var expected = new[]
        {
            new
            {
                Id = 1,
                Number = "000001",
                Name = "Mr Ted Crilly",
                Balance = 25
            },
            new
            {
                Id = 2,
                Number = "000002",
                Name = "Mr Fred Olsen",
                Balance = 250
            }
        };

        // add accounts

        _accountRepo.AddOrUpdate(new()
        {
            Name = expected[0].Name,
            Balance = expected[0].Balance
        });

        _accountRepo.AddOrUpdate(new()
        {
            Name = expected[1].Name,
            Balance = expected[1].Balance
        });

        // get accounts

        var accounts = _accountRepo.Get();

        accounts.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetById()
    {
        var expected = new
        {
            Id = 1,
            Number = "000001",
            Name = "Mr Ted Crilly",
            Balance = 25
        };

        // add

        _accountRepo.AddOrUpdate(new()
        {
            Name = expected.Name,
            Balance = expected.Balance
        });

        // get

        var account = _accountRepo.Get(expected.Id);

        account.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetByNumber()
    {
        var expected = new
        {
            Id = 1,
            Number = "000001",
            Name = "Mr Ted Crilly",
            Balance = 25
        };

        // add

        _accountRepo.AddOrUpdate(new()
        {
            Name = expected.Name,
            Balance = expected.Balance
        });

        // get

        var account = _accountRepo.Get(expected.Number);

        account.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Truncate()
    {
        // add

        _accountRepo.AddOrUpdate(new()
        {
            Name = "Mr Ted Crilly",
            Balance = 25
        });

        _accountRepo.Get().Any().Should().BeTrue();

        // truncate

        _accountRepo.Truncate();
        _accountRepo.Get().Any().Should().BeFalse();
    }    
}