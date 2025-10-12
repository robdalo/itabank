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
        return _accountRepo.AddOrUpdate(account);
    }

    public List<Account> Get()
    {
        return _accountRepo.Get();
    }

    public Account Get(int id)
    {
        return _accountRepo.Get(id);
    }

    public Account Get(string number)
    {
        return _accountRepo.Get(number);
    }

    public void Truncate()
    {
        _accountRepo.Truncate();
    }
}