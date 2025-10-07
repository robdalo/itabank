using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using itabank.Core.Repositories;

namespace itabank.Core.Tests;

public class AccountRepoTests
{
    private IFixture _autoFixture;

    private AccountRepo _accountRepo;

    [SetUp]
    public void Setup()
    {
        _autoFixture = new Fixture();
        _autoFixture.Customize(new AutoMoqCustomization());

        _accountRepo = _autoFixture.Create<AccountRepo>();
    }

    [Test]
    public void AddOrUpdate()
    {
        _accountRepo.Truncate();

        var account = _accountRepo.AddOrUpdate(new() {
            Name = "Mr Ted Crilly",
            Balance = 25          
        });

        account.Id.Should().Be(1);
        account.Number.Should().Be("000001");
        account.Name.Should().Be("Mr Ted Crilly");
        account.Balance.Should().Be(25);
    }
}