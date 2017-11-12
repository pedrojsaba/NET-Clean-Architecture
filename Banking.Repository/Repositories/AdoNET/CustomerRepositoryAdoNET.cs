using Banking.Domain.Repositories;
using System;


namespace Banking.Infrastructure.Repositories
{
    public class CustomerRepositoryAdoNET:ICustomerRepository
    {
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

        public System.Collections.Generic.List<Domain.Model.Customer> GetAll()
        {
            throw new NotImplementedException();
        }


        public Domain.Model.Customer GetByCustomerId(int CustomerId)
        {
            throw new NotImplementedException();
        }
    }
}
