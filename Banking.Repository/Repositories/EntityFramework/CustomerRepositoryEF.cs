using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking.Domain.Repositories;
using Banking.Infrastructure.Migrations;

namespace Banking.Infrastructure.Repositories.EntityFramework
{
    public class CustomerRepositoryEF : ICustomerRepository
    {

        BankingContext dbContext = new BankingContext(Funciones.GetConnectionString());

        public List<Banking.Domain.Model.Customer> GetAll()
        {

            var customers = from a in dbContext.Customers
                            select a;

            List<Banking.Domain.Model.Customer> lstCustomers = new List<Banking.Domain.Model.Customer>();
            foreach(Banking.Infrastructure.Migrations.Customer item in customers)
            {
                var viewModel = new Banking.Domain.Model.Customer();
                viewModel.Id = item.CustomerId;
                viewModel.FirstName = item.FirstName;
                viewModel.LastName = item.LastName;
                lstCustomers.Add(viewModel);
            }

            return lstCustomers;
        }


        public Banking.Domain.Model.Customer GetByCustomerId(int customerId)
        {

            Banking.Infrastructure.Migrations.Customer customer = (from a in dbContext.Customers
                            where a.CustomerId==customerId
                            select a).FirstOrDefault();

                var viewModel = new Banking.Domain.Model.Customer();
                viewModel.Id = customer.CustomerId;
                viewModel.FirstName = customer.FirstName;
                viewModel.LastName = customer.LastName;
                viewModel.Dni = customer.Dni;
                viewModel.Email = customer.Email;
                return viewModel;
        }

        public void save(Banking.Domain.Model.Customer entity)
        {
            throw new NotImplementedException();
        }

        public void update(Banking.Domain.Model.Customer entity)
        {
            throw new NotImplementedException();
        }

        public void merge(Banking.Domain.Model.Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
