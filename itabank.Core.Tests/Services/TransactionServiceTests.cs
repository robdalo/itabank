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
        var expected = new {
            Payer = "000001",
            Payee = "000002",
            Value = 150
        };

        _transactionService.Post(expected.Payer, expected.Payee, expected.Value);

        _accountRepo.Verify(x => x.Get(expected.Payer), Times.Once);
        _accountRepo.Verify(x => x.Get(expected.Payee), Times.Once);
        _accountRepo.Verify(x => x.AddOrUpdate(It.IsAny<Account>()), Times.Exactly(2));
    }
}