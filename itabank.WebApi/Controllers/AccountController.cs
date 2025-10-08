using itabank.Core.Domain.Models;
using itabank.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace itabank.WebApi.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ITransactionService _transactionService;

    public AccountController(
        IAccountService accountService,
        ITransactionService transactionService)
    {
        _accountService = accountService;
        _transactionService = transactionService;
    }

    [HttpGet]
    [Route("{accountNumber}")]
    public Account GetByNumber(string accountNumber)
    {
        return _accountService.GetByNumber(accountNumber);
    }
}