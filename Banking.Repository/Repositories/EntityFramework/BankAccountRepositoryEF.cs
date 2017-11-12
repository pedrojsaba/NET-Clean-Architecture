using Banking.Domain.Repositories;
using Banking.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Banking.Infrastructure.Repositories.EntityFramework
{
    public class BankAccountRepositoryEF : IBankAccountRepository
    {

        BankingContext dbContext = new BankingContext(Funciones.GetConnectionString());

        public Banking.Domain.Model.BankAccount FindByNumber(string accountNumber)
        {

            var account = (from a in dbContext.BankAccounts
                           join e in dbContext.Customers on a.CustomerId equals e.CustomerId
                           where a.Number.Equals(accountNumber)
                           select a).FirstOrDefault();

            var viewModel = new Banking.Domain.Model.BankAccount();
            if (account != null)
            {
                viewModel.Id = account.BankAccountId;
                viewModel.Number = account.Number;
                viewModel.Balance = (decimal)account.Balance;
                viewModel.Customer = new Banking.Domain.Model.Customer() { FirstName = account.customer.FirstName, LastName = account.customer.LastName };
            }
            else
            {
                viewModel.Customer = new Banking.Domain.Model.Customer() { FirstName = "El número de cuenta no existe ", LastName = "!" };
            }

            return viewModel;

        }

        public List<Banking.Domain.Model.BankAccount> GetByCustomerId(int customerId)
        {
            var accounts = from a in dbContext.BankAccounts
                           where a.CustomerId==customerId
                            select a;

            List<Banking.Domain.Model.BankAccount> lstAccounts = new List<Banking.Domain.Model.BankAccount>();
            foreach (Banking.Infrastructure.Migrations.BankAccount item in accounts)
            {
                var viewModel = new Banking.Domain.Model.BankAccount();
                viewModel.Id = item.BankAccountId;
                viewModel.Number = item.Number;
                viewModel.Balance = (decimal)item.Balance;
                lstAccounts.Add(viewModel);
            }

            return lstAccounts;
        }

        public Banking.Domain.Model.BankAccount FindById(int BankAccountId)
        {
            var account = (from a in dbContext.BankAccounts
                           where a.BankAccountId == BankAccountId
                           select a).FirstOrDefault();

            var viewModel = new Domain.Model.BankAccount
            {
                Id = account.BankAccountId,
                Number = account.Number,
                Balance = (decimal) account.Balance
            };

            return viewModel;
        }


        public Banking.Domain.Model.BankAccount FindByNumberLocked(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public void save(Banking.Domain.Model.BankAccount entity)
        {
            throw new NotImplementedException();
        }

        public void update(Banking.Domain.Model.BankAccount entity)
        {

            var account = (from a in dbContext.BankAccounts
                           where a.BankAccountId == entity.Id
                           select a).FirstOrDefault();

            account.Balance = entity.Balance;
            dbContext.SaveChanges();

        }

        public void merge(Banking.Domain.Model.BankAccount entity)
        {
            throw new NotImplementedException();
        }


    
    }
}
