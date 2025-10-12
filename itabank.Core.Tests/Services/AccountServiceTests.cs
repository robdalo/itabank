using AutoFixture;
using AutoFixture.AutoMoq;
using itabank.Core.Domain.Models;
using itabank.Core.Repositories.Interfaces;
using Moq;

namespace itabank.Core.Services.Tests;

public class AccountServiceTests
{
    private IFixture _autoFixture;

    private AccountService _accountService;

    private Mock<IAccountRepo> _accountRepo;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _autoFixture = new Fixture();
        _autoFixture.Customize(new AutoMoqCustomization());

        _accountRepo = _autoFixture.Freeze<Mock<IAccountRepo>>();

        _accountService = _autoFixture.Create<AccountService>();
    }

    [Test]
    public void AddOrUpdate()
    {
        // add

        _accountService.AddOrUpdate(new()
        {
            Name = "Mr Ted Crilly",
            Balance = 25
        });

        _accountRepo.Verify(x => x.AddOrUpdate(It.IsAny<Account>()), Times.Once);
    }
    
    [Test]
    public void Get()
    {
        _accountService.Get();
        _accountRepo.Verify(x => x.Get(), Times.Once);
    }    

    [Test]
    public void GetById()
    {
        _accountService.Get(1);
        _accountRepo.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void GetByNumber()
    {
        _accountService.Get("000001");
        _accountRepo.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void Truncate()
    {
        _accountService.Truncate();
        _accountRepo.Verify(x => x.Truncate(), Times.Once);
    }    
}