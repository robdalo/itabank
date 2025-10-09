using itabank.Core.Domain.Models;

namespace itabank.Core.Services.Interfaces;

public interface IAccountService
{
    Account AddOrUpdate(Account account);
    List<Account> Get();
    Account Get(int id);
    Account Get(string number);
    void Truncate();
}