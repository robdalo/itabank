namespace itabank.Core.Services.Interfaces;

public interface ITransactionService
{
    void Post(string payer, string payee, decimal value);
}