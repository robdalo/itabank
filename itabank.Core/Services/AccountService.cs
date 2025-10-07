using itabank.Core.Domain.Models;
using itabank.Core.Repositories.Interfaces;
using itabank.Core.Services.Interfaces;

namespace itabank.Core.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepo _accountRepo;

    public AccountService(IAccountRepo accountRepo)
    {
        _accountRepo = accountRepo;
    }

    public Account AddOrUpdate(Account account)
    {
        if (account.Id < 1)
            CreateAccountNumber(account);
            
        return _accountRepo.AddOrUpdate(account);
    }

    public Account Get(int accountId)
    {
        return _accountRepo.Get(accountId);
    }

    internal void CreateAccountNumber(Account account)
    {
        account.Id = GetAccountId(account);
        account.Number = $"{account.Id:D6}";
    }

    private int GetAccountId(Account account)
    {
        return account.Transactions.Any() ?
            account.Transactions.Max(x => x.Id) + 1 : 1;
    }
}