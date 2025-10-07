using itabank.Core.Domain.Models;
using itabank.Core.Repositories.Interfaces;
using LiteDB;

namespace itabank.Core.Repositories;

public class AccountRepo : IAccountRepo
{
    private const string DatabaseName = "itabank.db";

    public Account AddOrUpdate(Account account)
    {
        using var context = new LiteDatabase(DatabaseName);

        var collection = context.GetCollection<Account>();

        var existing = collection.FindOne(x => x.Number == account.Number);

        if (existing == null)
            existing = new Account();

        existing.Name = account.Name;
        existing.Number = account.Number;
        existing.Balance = account.Balance;
        existing.Transactions = account.Transactions;

        collection.Upsert(existing);

        return existing;
    }

    public Account Get(int id)
    {
        using var context = new LiteDatabase(DatabaseName);

        var collection = context.GetCollection<Account>();

        return collection.FindById(id);
    }

    public List<Account> Get()
    {
        using var context = new LiteDatabase(DatabaseName);

        var collection = context.GetCollection<Account>();

        return collection.FindAll().ToList();
    }

    public Account GetByNumber(string number)
    {
        using var context = new LiteDatabase(DatabaseName);

        var collection = context.GetCollection<Account>();

        return collection.FindOne(x => x.Number == number);
    }

    public void Truncate()
    {
        using var context = new LiteDatabase(DatabaseName);

        var collection = context.GetCollection<Account>();

        collection.DeleteAll();
    }
}