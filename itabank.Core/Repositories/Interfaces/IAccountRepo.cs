using itabank.Core.Domain.Models;

namespace itabank.Core.Repositories.Interfaces;

public interface IAccountRepo
{
    Account AddOrUpdate(Account account);
    List<Account> Get();
    Account Get(int id);
    Account GetByNumber(string number);
    void Truncate();
}