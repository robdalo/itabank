using AutoFixture;
using AutoFixture.AutoMoq;
using itabank.Core.Domain.Models;
using itabank.Core.Repositories.Interfaces;
using Moq;

namespace itabank.Core.Services.Tests;

public class TransactionServiceTests
{
    private IFixture _autoFixture;

    private TransactionService _transactionService;

    private Mock<IAccountRepo> _accountRepo;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _autoFixture = new Fixture();
        _autoFixture.Customize(new AutoMoqCustomization());

        _accountRepo = _autoFixture.Freeze<Mock<IAccountRepo>>();

        _transactionService = _autoFixture.Create<TransactionService>();
    }

    [Test]
    public void Post()
    {
        var accountNumberDebit = "000001";
        var accountNumberCredit = "000002";
        var value = 150;

        _transactionService.Post(accountNumberDebit, accountNumberCredit, value);

        _accountRepo.Verify(x => x.GetByNumber(accountNumberDebit), Times.Once);
        _accountRepo.Verify(x => x.GetByNumber(accountNumberCredit), Times.Once);
        _accountRepo.Verify(x => x.AddOrUpdate(It.IsAny<Account>()), Times.Exactly(2));
    }
}