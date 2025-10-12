using AutoMapper;
using itabank.Core.Services.Interfaces;
using itabank.SDK.Requests;
using itabank.Shared;
using Microsoft.AspNetCore.Mvc;

using Domain = itabank.Core.Domain;
using SDK = itabank.SDK;

namespace itabank.WebApi.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ILogger<AccountController> _logger;
    private readonly IMapper _mapper;
    private readonly ITransactionService _transactionService;

    public AccountController(
        IAccountService accountService,
        ILogger<AccountController> logger,
        IMapper mapper,
        ITransactionService transactionService)
    {
        _accountService = accountService;
        _logger = logger;
        _mapper = mapper;
        _transactionService = transactionService;
    }

    [HttpPost]
    [Route("")]
    public SDK.Models.Account AddOrUpdate(SDK.Requests.AccountRequest request)
    {
        _logger.LogInformation($"Add or update account - {Serializer.Serialize(request)}");

        var account = _accountService.AddOrUpdate(new() {
            Number = request.Number,
            Name = request.Name,
            Balance = request.Balance
        });

        return _mapper.Map<SDK.Models.Account>(account);
    }

    [HttpGet]
    [Route("")]
    public List<SDK.Models.Account> Get()
    {
        _logger.LogInformation($"Get accounts");

        return _mapper.Map<List<SDK.Models.Account>>(_accountService.Get());
    }

    [HttpGet]
    [Route("id/{accountId}")]
    public SDK.Models.Account Get(int accountId)
    {
        _logger.LogInformation($"Get account - accountId: {accountId}");

        return _mapper.Map<SDK.Models.Account>(_accountService.Get(accountId));
    }

    [HttpGet]
    [Route("number/{accountNumber}")]
    public SDK.Models.Account Get(string accountNumber)
    {
        _logger.LogInformation($"Get account - accountNumber: {accountNumber}");

        return _mapper.Map<SDK.Models.Account>(_accountService.Get(accountNumber));
    }

    [HttpPost]
    [Route("transaction/post")]
    public void Post(TransactionRequest request)
    {
        _logger.LogInformation($"Post transaction - {Serializer.Serialize(request)}");

        _transactionService.Post(
            payer: request.Payer,
            payee: request.Payee,
            value: request.Value);
    }

    [HttpPut]
    [Route("truncate")]
    public void Truncate()
    {
        _logger.LogInformation($"Truncate database");

        _accountService.Truncate();
    }
}