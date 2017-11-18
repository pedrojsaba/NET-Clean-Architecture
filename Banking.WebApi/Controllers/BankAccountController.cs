using System.Linq;
using System.Web.Http;
using Banking.Application;
using Banking.Application.Dto;
using Microsoft.AspNet.Identity;

namespace Banking.WebApi.Controllers
{
    [RoutePrefix("api/BankAccount")]
    public class BankAccountController : ApiController
    {        
        // POST api/BankAccount/GetAccounts/5        
        [HttpPost]
        [Authorize(Roles = "administrator")]
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

        // GET api/BankAccount/GetAccounts     
        [HttpGet]
        [Authorize(Roles = "administrator")]
        public BankAccountDto[] GetAccounts()
        {
            var id = User.Identity.GetUserName();
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
