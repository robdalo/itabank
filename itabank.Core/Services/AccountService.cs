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

    public Account Get(int id)
    {
        return _accountRepo.Get(id);
    }
    
    public Account GetByNumber(string number)
    {
        return _accountRepo.GetByNumber(number);
    }
}