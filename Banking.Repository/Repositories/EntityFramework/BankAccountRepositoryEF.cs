using Banking.Domain.Repositories;
using Banking.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Banking.Infrastructure.Repositories.EntityFramework
{
    // ReSharper disable once InconsistentNaming
    public class BankAccountRepositoryEF : IBankAccountRepository
    {
        readonly BankingContext _dbContext = new BankingContext(Funciones.GetConnectionString());

        public Domain.Model.BankAccount FindByNumber(string accountNumber)
        {

            var account = (from a in _dbContext.BankAccounts
                           join e in _dbContext.Customers on a.CustomerId equals e.CustomerId
                           where a.Number.Equals(accountNumber)
                           select a).FirstOrDefault();

            var viewModel = new Domain.Model.BankAccount();
            if (account != null)
            {
                viewModel.Id = account.BankAccountId;
                viewModel.Number = account.Number;
                viewModel.Balance = account.Balance??0;
                viewModel.Customer = new Domain.Model.Customer { FirstName = account.customer.FirstName, LastName = account.customer.LastName };
            }
            else
            {
                viewModel.Customer = new Domain.Model.Customer { FirstName = "El número de cuenta no existe ", LastName = "!" };
            }

            return viewModel;

        }

        public List<Domain.Model.BankAccount> GetByCustomerId(int customerId)
        {
            var accounts = from a in _dbContext.BankAccounts
                           where a.CustomerId == customerId
                           select a;

            var lstAccounts = new List<Domain.Model.BankAccount>();
            foreach (var item in accounts)
            {
                var viewModel = new Domain.Model.BankAccount
                {
                    Id = item.BankAccountId,
                    Number = item.Number,
                    Balance = item.Balance??0
                };
                lstAccounts.Add(viewModel);
            }

            return lstAccounts;
        }
        public List<Domain.Model.BankAccount> GetByUsername(string username)
        {
            var res = (from cust in _dbContext.Customers
                       join bacc in _dbContext.BankAccounts
                            on cust.CustomerId equals bacc.CustomerId
                       where cust.Dni.Equals(username)
                       orderby bacc.Number
                       select new Domain.Model.BankAccount
                       {
                           Id = bacc.BankAccountId,
                           Number = bacc.Number,
                           Balance = (decimal)bacc.Balance
                       }).ToList();

            return res;
        }



        public Domain.Model.BankAccount FindById(int bankAccountId)
        {            
            var account = _dbContext.BankAccounts.FirstOrDefault(f => f.BankAccountId.Equals(bankAccountId));

            if (account == null) return new Domain.Model.BankAccount();

            var viewModel = new Domain.Model.BankAccount
            {
                Id = account.BankAccountId,
                Number = account.Number,
                Balance = account.Balance ?? 0
            };

            return viewModel;
        }

        public bool AccountEnabled(string accountNumber)
        {
            return _dbContext.BankAccounts.Any(f => f.Number.Equals(accountNumber) && f.IsLocked == false);
        }

        public Domain.Model.BankAccount FindByNumberLocked(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public void save(Domain.Model.BankAccount entity)
        {
            throw new NotImplementedException();
        }

        public void update(Domain.Model.BankAccount entity)
        {

            var account = (from a in _dbContext.BankAccounts
                           where a.BankAccountId == entity.Id
                           select a).FirstOrDefault();

            if (account != null) account.Balance = entity.Balance;
            _dbContext.SaveChanges();

        }

        public void merge(Domain.Model.BankAccount entity)
        {
            throw new NotImplementedException();
        }



    }
}
