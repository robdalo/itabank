using itabank.Core.Domain.Models;

namespace itabank.Core.Services.Interfaces;

public interface IAccountService
{
    Account AddOrUpdate(Account account);
    Account Get(int accountId);
}