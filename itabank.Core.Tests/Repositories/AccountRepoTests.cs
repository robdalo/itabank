using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using itabank.Core.Repositories;

namespace itabank.Core.Repositories.Tests;

public class AccountRepoTests
{
    private IFixture _autoFixture;

    private AccountRepo _accountRepo;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _autoFixture = new Fixture();
        _autoFixture.Customize(new AutoMoqCustomization());

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

        var account = _accountRepo.GetByNumber("000001");

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

        _accountRepo.GetByNumber("000001").Should().NotBeNull();

        // truncate

        _accountRepo.Truncate();
        _accountRepo.GetByNumber("000001").Should().BeNull();
    }    
}