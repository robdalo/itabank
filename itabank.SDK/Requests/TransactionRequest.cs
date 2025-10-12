namespace itabank.SDK.Requests;

public class TransactionRequest
{
    public string Payer { get; set; }
    public string Payee { get; set; }
    public decimal Value { get; set; }
}