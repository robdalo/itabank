namespace itabank.SDK.Models;

public class Account
{
    public int Id { get; set; }
    public string Number { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; } = 0;
    public List<Transaction> Transactions { get; set; } = new();
}