namespace itabank.Shared.Config;

public static class ApiEndpoints
{
    public static string AddOrUpdateAccount = "account";
    public static string GetAccounts = "account";
    public static string GetAccountById = "account/id/{accountId}";
    public static string GetAccountByNumber = "account/number/{accountNumber}";
    public static string PostTransaction = "account/transaction/post";
    public static string TruncateAccounts = "account/truncate";
}