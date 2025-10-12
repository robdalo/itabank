using itabank.SDK.Models;
using itabank.SDK.Requests;

namespace itabank.SDK.Consumers.Interfaces;

public interface IApiConsumer
{
    Task<Account> AddAccountAsync(AccountRequest request);
    Task<List<Account>> GetAccountsAsync();
    Task<Account> GetAccountAsync(int accountId);
    Task<Account> GetAccountAsync(string accountNumber);
    Task PostTransactionAsync(TransactionRequest request);
    Task TruncateAsync();
    Task<Account> UpdateAccountAsync(AccountRequest request);
}