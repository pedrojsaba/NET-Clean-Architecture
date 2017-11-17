using Banking.Application;
using Banking.Application.Dto;
using System.Linq;
using System.Web.Http;

namespace Banking.WebApi.Controllers
{
    public class BankAccountController : ApiController
    {
        //http://localhost:10559/api/BankAccount/GetAccounts?id=5
        [HttpGet]
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
