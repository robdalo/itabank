using itabank.Core.Domain.Models;

namespace itabank.Core.Repositories.Interfaces;

public interface IAccountRepo
{
    Account AddOrUpdate(Account account);
    List<Account> Get();
    Account Get(int accountId);
    void Truncate();
}