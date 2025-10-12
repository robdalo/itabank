using itabank.Core.Domain.Enums;
using itabank.Core.Domain.Models;
using itabank.Core.Repositories.Interfaces;
using itabank.Core.Services.Interfaces;

namespace itabank.Core.Services;

public class TransactionService : ITransactionService
{
    private readonly IAccountRepo _accountRepo;

    public TransactionService(IAccountRepo accountRepo)
    {
        _accountRepo = accountRepo;
    }

    public void Post(string payer, string payee, decimal value)
    {
        var timestamp = DateTime.UtcNow;

        // debit account

        var payerAccount = _accountRepo.Get(payer);
        var payeeAccount = _accountRepo.Get(payee);

        payerAccount.Transactions.Add(new()
        {
            Id = GetTransactionId(payerAccount),
            Timestamp = timestamp,
            AccountId = payeeAccount.Id,
            Type = TransactionType.Debit,
            Value = value
        });

        payerAccount.Balance -= value;

        // credit account

        payeeAccount.Transactions.Add(new()
        {
            Id = GetTransactionId(payeeAccount),
            Timestamp = timestamp,
            AccountId = payerAccount.Id,
            Type = TransactionType.Credit,
            Value = value
        });

        payeeAccount.Balance += value;

        // write to database

        _accountRepo.AddOrUpdate(payerAccount);
        _accountRepo.AddOrUpdate(payeeAccount);
    }

    private int GetTransactionId(Account account)
    {
        return account.Transactions.Any() ?
            account.Transactions.Max(x => x.Id) + 1 : 1;
    }
}