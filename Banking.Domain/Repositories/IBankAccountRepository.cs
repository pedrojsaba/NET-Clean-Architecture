using Banking.Domain.Model;
using System.Collections.Generic;

namespace Banking.Domain.Repositories
{
    public interface IBankAccountRepository : IGenericRepository<BankAccount>
    {
        BankAccount FindByNumber(string accountNumber);
        BankAccount FindByNumberLocked(string accountNumber);
        bool AccountEnabled(string accountNumber);
        List<BankAccount> GetByCustomerId(int customerId);
        List<BankAccount> GetByUsername(string username);
        BankAccount FindById(int bankAccountId);
        bool InsufficientBalance(string accountNumber, decimal amount);
    }

}
