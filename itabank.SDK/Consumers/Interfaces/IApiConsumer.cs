using itabank.SDK.Models;

namespace itabank.SDK.Consumers.Interfaces;

public interface IApiConsumer
{
    Task<Account> GetAccountByNumberAsync(string accountNumber);
}
