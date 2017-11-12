using Banking.Infrastructure.Migrations;

namespace Banking.Infrastructure.Repositories.EntityFramework
{

    public class EfRepository
    {
        protected readonly BankingContext DbContext;

        public EfRepository(BankingContext dbContext)
        {
            DbContext = dbContext;
        }

    }
}
