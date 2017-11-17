using System.Linq;
using System.Web.Http;
using Banking.Application;
using Banking.Application.Dto;

namespace Banking.WebApi.Controllers
{
    public class OperationController : ApiController
    {

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public CustomerDto[] Get()
        {
            return new BankingApplicationService().GetAll().Select(x => new CustomerDto
            {
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToArray();
        }

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public CustomerDto GetByCustomerId(int id)
        {
            var bankingApplicationService = new BankingApplicationService();
            var byCustomerId = bankingApplicationService.GetByCustomerId(id);
            return new CustomerDto
            {
                FirstName = byCustomerId.FirstName,
                LastName = byCustomerId.LastName
            };
        }
    }
}