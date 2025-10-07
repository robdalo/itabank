using itabank.Core.Domain.Enums;

namespace itabank.Core.Domain.Models;

public class Transaction
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public int AccountId { get; set; }
    public TransactionType Type { get; set; }
    public decimal Value { get; set; }
}