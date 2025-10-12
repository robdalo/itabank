using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using itabank.Core.Domain.Models;
using itabank.Core.Services.Interfaces;
using itabank.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
            logger: NullLoggerFactory.Instance.CreateLogger<AccountController>(),
            mapper: _mapper.Object,
            transactionService: _transactionService.Object);
    }

    [Test]
    public void AddOrUpdate()
    {
        _controller
            .Invoking(x => x.AddOrUpdate(new() {
                Name = "Mr Ted Crilly",
                Balance = 25 }))
            .Should()
            .NotThrow();

        _accountService.Verify(x => x.AddOrUpdate(It.IsAny<Account>()), Times.Once);
    }

    [Test]
    public void Get()
    {
        _controller.Invoking(x => x.Get())
                   .Should()
                   .NotThrow();

        _accountService.Verify(x => x.Get(), Times.Once);
    }  

    [Test]
    public void GetById()
    {
        _controller.Invoking(x => x.Get(1))
                   .Should()
                   .NotThrow();

        _accountService.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void GetByNumber()
    {
        _controller.Invoking(x => x.Get("000001"))
                   .Should()
                   .NotThrow();

        _accountService.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Post()
    {
        _controller
            .Invoking(x => x.Post(new() {
                Payer = "000001",
                Payee = "000002",
                Value = 125 }))
            .Should()
            .NotThrow();

        _transactionService.Verify(x => x.Post(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>()), Times.Once);
    }
    
    [Test]
    public void Truncate()
    {
        _controller.Invoking(x => x.Truncate())
                   .Should()
                   .NotThrow();

        _accountService.Verify(x => x.Truncate(), Times.Once);
    }    
}
