using Banking.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Infrastructure.Repositories.EntityFramework
{

    public class EfRepository
    {
        protected readonly BankingContext _dbContext;

        public EfRepository(BankingContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
