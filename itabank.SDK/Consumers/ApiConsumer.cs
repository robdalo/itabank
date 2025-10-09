using itabank.SDK.Consumers.Interfaces;
using itabank.SDK.Models;
using itabank.SDK.Requests;
using itabank.SDK.Settings;
using itabank.Shared;
using itabank.Shared.Config;
using Microsoft.Extensions.Options;

namespace itabank.SDK.Consumers;

public class ApiConsumer : IApiConsumer
{
    private readonly ApiSettings _settings;
    private readonly RestConsumer _restConsumer;

    private const string AuthToken = "";

    public ApiConsumer(IOptions<ApiSettings> settings)
    {
        _settings = settings.Value;
        _restConsumer = new RestConsumer(_settings.BaseUrl);
    }

    public async Task<Account> AddOrUpdateAccountAsync(AccountRequest request)
    {
        return await _restConsumer.PostAsync<Account>(
            endpoint: ApiEndpoints.AddOrUpdateAccount,
            authToken: AuthToken,
            payload: request);
    }

    public async Task<List<Account>> GetAccountsAsync()
    {
        return await _restConsumer.GetAsync<List<Account>>(
            endpoint: ApiEndpoints.GetAccounts,
            authToken: AuthToken);
    }

    public async Task<Account> GetAccountAsync(int accountId)
    {
        return await _restConsumer.GetAsync<Account>(
            endpoint: ApiEndpoints.GetAccountById.Replace("{accountId}", accountId.ToString()),
            authToken: AuthToken);
    }

    public async Task<Account> GetAccountAsync(string accountNumber)
    {
        return await _restConsumer.GetAsync<Account>(
            endpoint: ApiEndpoints.GetAccountByNumber.Replace("{accountNumber}", accountNumber),
            authToken: AuthToken);
    }

    public async Task PostTransactionAsync(TransactionRequest request)
    {
        await _restConsumer.PostAsync<Account>(
            endpoint: ApiEndpoints.PostTransaction,
            authToken: AuthToken,
            payload: request);
    }

    public async Task TruncateAsync()
    {
        await _restConsumer.PutAsync(
            endpoint: ApiEndpoints.TruncateAccounts,
            authToken: AuthToken);
    }
}