using Banking.Domain.Model;
using System.Collections.Generic;

namespace Banking.Domain.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {

        List<Customer> GetAll();
        Customer GetByCustomerId(int customerId);
    }
}
