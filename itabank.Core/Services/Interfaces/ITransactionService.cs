namespace itabank.Core.Services.Interfaces;

public interface ITransactionService
{
    void Post(string accountNumberDebit, string accountNumberCredit, decimal value);
}