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

    public void Post(
        string accountNumberDebit,
        string accountNumberCredit,
        decimal value)
    {
        var timestamp = DateTime.UtcNow;

        // debit account

        var debitAccount = _accountRepo.GetByNumber(accountNumberDebit);
        var creditAccount = _accountRepo.GetByNumber(accountNumberCredit);

        debitAccount.Transactions.Add(new()
        {
            Id = GetTransactionId(debitAccount),
            Timestamp = timestamp,
            AccountId = creditAccount.Id,
            Type = TransactionType.Debit,
            Value = value
        });

        debitAccount.Balance -= value;

        // credit account

        creditAccount.Transactions.Add(new()
        {
            Id = GetTransactionId(creditAccount),
            Timestamp = timestamp,
            AccountId = debitAccount.Id,
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