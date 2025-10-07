namespace itabank.Core.Services.Interfaces;

public interface ITransactionService
{
    void Post(int debitAccountId, int creditAccountId, decimal value);
}