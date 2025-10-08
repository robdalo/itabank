using itabank.SDK.Models;
using itabank.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

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

    [HttpGet]
    [Route("{accountNumber}")]
    public Account GetByNumber(string accountNumber)
    {
        return _mapper.Map<Account>(_accountService.GetByNumber(accountNumber));
    }
}