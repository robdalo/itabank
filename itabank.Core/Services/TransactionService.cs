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

    public void Post(int debitAccountId, int creditAccountId, decimal value)
    {
        var timestamp = DateTime.UtcNow;

        // debit account

        var debitAccount = _accountRepo.Get(debitAccountId);

        debitAccount.Transactions.Add(new()
        {
            Id = GetTransactionId(debitAccount),
            Timestamp = timestamp,
            AccountId = creditAccountId,
            Type = TransactionType.Debit,
            Value = value
        });

        debitAccount.Balance -= value;

        // credit account

        var creditAccount = _accountRepo.Get(creditAccountId);

        creditAccount.Transactions.Add(new()
        {
            Id = GetTransactionId(creditAccount),
            Timestamp = timestamp,
            AccountId = debitAccountId,
            Type = TransactionType.Credit,
            Value = value
        });

        creditAccount.Balance += value;

        // write to database

        _accountRepo.AddOrUpdate(debitAccount);
        _accountRepo.AddOrUpdate(creditAccount);
    }

    internal int GetTransactionId(Account account)
    {
        return account.Transactions.Any() ?
            account.Transactions.Max(x => x.Id) + 1 : 1;
    }
}