using System;
using System.Collections.Generic;
using System.Linq;
using Banking.Domain.Repositories;
using Banking.Infrastructure.Migrations;

// ReSharper disable once CheckNamespace
namespace Banking.Infrastructure.Repositories.EntityFramework
{
    // ReSharper disable once InconsistentNaming
    public class CustomerRepositoryEF : ICustomerRepository
    {
        readonly BankingContext _dbContext = new BankingContext(Funciones.GetConnectionString());

        public List<Domain.Model.Customer> GetAll()
        {
            return _dbContext.Customers.Select(r => new Domain.Model.Customer
            {
                Id = r.CustomerId,
                FirstName = r.FirstName,
                LastName = r.LastName
            }).ToList();
        }


        public Domain.Model.Customer GetByCustomerId(int customerId)
        {

            var customer = (from a in _dbContext.Customers
                            where a.CustomerId == customerId
                            select a).FirstOrDefault();

            if (customer == null) return new Domain.Model.Customer();

            var viewModel = new Domain.Model.Customer
            {
                Id = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Dni = customer.Dni,
                Email = customer.Email
            };
            return viewModel;

        }

        public void save(Domain.Model.Customer entity)
        {
            throw new NotImplementedException();
        }

        public void update(Domain.Model.Customer entity)
        {
            throw new NotImplementedException();
        }

        public void merge(Domain.Model.Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
