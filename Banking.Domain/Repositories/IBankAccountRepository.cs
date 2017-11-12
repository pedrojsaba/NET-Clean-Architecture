using Banking.Domain.Model;
using System.Collections.Generic;

namespace Banking.Domain.Repositories
{
    public interface IBankAccountRepository : IGenericRepository<BankAccount>
    {
        BankAccount FindByNumber(string accountNumber);
        BankAccount FindByNumberLocked(string accountNumber);

        List<BankAccount> GetByCustomerId(int customerId);
        BankAccount FindById(int bankAccountId);

    }

}
