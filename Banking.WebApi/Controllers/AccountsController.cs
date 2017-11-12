using Banking.Application;
using System.Web.Http;
using Banking.Application.Dto;

namespace Banking.WebApi.Controllers
{
    public class AccountsController : ApiController
    {

        //api/Accounts/5
        [HttpGet]
        public BankAccountDto FindById(int id)
        {
            var bankingApplicationService = new BankingApplicationService();
            var bankAccount = bankingApplicationService.FindById(id);
            return new BankAccountDto
            {
                Balance = bankAccount.Balance,
                Number = bankAccount.Number,
                Id = bankAccount.Id,
                IsLocked = bankAccount.IsLocked
            };
        }

    }
}