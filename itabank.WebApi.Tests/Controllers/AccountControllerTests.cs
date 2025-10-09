using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using itabank.Core.Services.Interfaces;
using itabank.WebApi.Controllers;
using Moq;

namespace itabank.WebApi.Tests;

public class AccountControllerTests
{
    private IFixture _autoFixture;

    private AccountController _controller;

    private Mock<IAccountService> _accountService;
    private Mock<IMapper> _mapper;
    private Mock<ITransactionService> _transactionService;

    [SetUp]
    public void Setup()
    {
        _autoFixture = new Fixture();
        _autoFixture.Customize(new AutoMoqCustomization());

        _accountService = _autoFixture.Freeze<Mock<IAccountService>>();
        _mapper = _autoFixture.Freeze<Mock<IMapper>>();
        _transactionService = _autoFixture.Freeze<Mock<ITransactionService>>();

        _controller = new AccountController(
            accountService: _accountService.Object,
            mapper: _mapper.Object,
            transactionService: _transactionService.Object);
    }

    [Test]
    public void GetByNumber()
    {
        _controller.Invoking(x => x.Get("000001"))
                   .Should()
                   .NotThrow();

        _accountService.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
    }
}
