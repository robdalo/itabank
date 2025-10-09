using AutoMapper;
using itabank.Core.Services.Interfaces;
using itabank.SDK.Requests;
using Microsoft.AspNetCore.Mvc;

using Domain = itabank.Core.Domain;
using SDK = itabank.SDK;

namespace itabank.WebApi.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    private readonly ITransactionService _transactionService;

    public AccountController(
        IAccountService accountService,
        IMapper mapper,
        ITransactionService transactionService)
    {
        _accountService = accountService;
        _mapper = mapper;
        _transactionService = transactionService;
    }

    [HttpPost]
    [Route("")]
    public SDK.Models.Account AddOrUpdate(SDK.Requests.AccountRequest request)
    {
        var account = _accountService.AddOrUpdate(new() {
            Number = request.Number,
            Name = request.Name
        });

        return _mapper.Map<SDK.Models.Account>(account);
    }

    [HttpGet]
    [Route("")]
    public List<SDK.Models.Account> Get()
    {
        return _mapper.Map<List<SDK.Models.Account>>(_accountService.Get());
    }

    [HttpGet]
    [Route("id/{accountId}")]
    public List<SDK.Models.Account> Get(int accountId)
    {
        return _mapper.Map<List<SDK.Models.Account>>(_accountService.Get(accountId));
    }

    [HttpGet]
    [Route("number/{accountNumber}")]
    public SDK.Models.Account Get(string accountNumber)
    {
        return _mapper.Map<SDK.Models.Account>(_accountService.Get(accountNumber));
    }

    [HttpPost]
    [Route("transaction/post")]
    public void Post(TransactionRequest request)
    {
        _transactionService.Post(
            accountNumberDebit: request.PayerAccountNumber,
            accountNumberCredit: request.PayeeAccountNumber,
            value: request.Value);
    }

    [HttpPut]
    [Route("truncate")]
    public void Truncate()
    {
        _accountService.Truncate();
    }
}