namespace itabank.SDK.Requests;

public class TransactionRequest
{
    public string PayerAccountNumber { get; set; }
    public string PayeeAccountNumber { get; set; }
    public decimal Value { get; set; }
}