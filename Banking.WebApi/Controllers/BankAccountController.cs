using System.Linq;
using System.Web.Http;
using Banking.Application;
using Banking.Application.Dto;

namespace Banking.WebApi.Controllers
{
    [RoutePrefix("api/BankAccount")]
    public class BankAccountController : ApiController
    {        
        // POST api/BankAccount/GetAccounts/5        
        [HttpPost]
        [Authorize(Roles = "client")]
        public BankAccountDto[] GetAccounts(int id)
        {
            return new BankingApplicationService().GetAccounts(id).Select(bankAccount => new BankAccountDto
            {
                Balance = bankAccount.Balance,
                Id = bankAccount.Id,
                Number = bankAccount.Number,
                IsLocked = bankAccount.IsLocked
            }).ToArray();
        }
    }
}
