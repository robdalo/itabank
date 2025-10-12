namespace itabank.SDK.Settings;

public class ApiSettings
{
    public string BaseUrl { get; set; }
    public static Dictionary<string, string> Endpoints => new()
    {
        { "AddAccount", "account" },
        { "GetAccounts", "account" },
        { "GetAccountById", "account/id/{accountId}" },
        { "GetAccountByNumber", "account/number/{accountNumber}" },
        { "PostTransaction", "account/transaction/post" },
        { "TruncateAccounts", "account/truncate" },
        { "UpdateAccount", "account" }
    };
}