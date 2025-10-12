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
        // add

        var account = _accountRepo.AddOrUpdate(new()
        {
            Name = "Mr Ted Crilly",
            Balance = 25
        });

        account.Id.Should().Be(1);
        account.Number.Should().Be("000001");
        account.Name.Should().Be("Mr Ted Crilly");
        account.Balance.Should().Be(25);

        // update

        account.Name = "Mr Jack Hackett";
        account.Balance += 100;

        account = _accountRepo.AddOrUpdate(account);

        account.Id.Should().Be(1);
        account.Number.Should().Be("000001");
        account.Name.Should().Be("Mr Jack Hackett");
        account.Balance.Should().Be(125);

        // add

        account = _accountRepo.AddOrUpdate(new()
        {
            Name = "Mr Larry Duff",
            Balance = 50
        });

        account.Id.Should().Be(2);
        account.Number.Should().Be("000002");
        account.Name.Should().Be("Mr Larry Duff");
        account.Balance.Should().Be(50);
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
        // add

        _accountRepo.AddOrUpdate(new()
        {
            Name = "Mr Ted Crilly",
            Balance = 25
        });

        // get

        var account = _accountRepo.Get(1);

        account.Id.Should().Be(1);
        account.Number.Should().Be("000001");
        account.Name.Should().Be("Mr Ted Crilly");
        account.Balance.Should().Be(25);
    }

    [Test]
    public void GetByNumber()
    {
        // add

        _accountRepo.AddOrUpdate(new()
        {
            Name = "Mr Ted Crilly",
            Balance = 25
        });

        // get

        var account = _accountRepo.Get("000001");

        account.Id.Should().Be(1);
        account.Number.Should().Be("000001");
        account.Name.Should().Be("Mr Ted Crilly");
        account.Balance.Should().Be(25);
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