using itabank.SDK.Consumers.Interfaces;
using itabank.SDK.Models;
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

    public async Task<Account> GetAccountByNumberAsync(string accountNumber)
    {
        return await _restConsumer.GetAsync<Account>(
            endpoint: ApiEndpoints.GetAccountByNumber.Replace("{accountNumber}", accountNumber),
            authToken: AuthToken);
    }
}