
using Banking.Infrastructure.Migrations;

namespace Banking.Application
{
    public class DataBaseService
    {
        public void CreateBanking(string nameOrConnectionString)
        {
            var banking = new BankingContext(nameOrConnectionString);
            banking.RunMigrations();
        }

    }
}
