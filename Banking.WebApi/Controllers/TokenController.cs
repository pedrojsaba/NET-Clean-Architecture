using Banking.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Banking.MvcService.Controllers
{
    public class TokenController : ApiController
    {

        //http://localhost:10559/api/Accounts/5
        [HttpGet]
        public Banking.Domain.Model.BankAccount[] FindById(int id)
        {
            var bankingApplicationService = new BankingApplicationService();
            Banking.Domain.Model.BankAccount account;
            account = bankingApplicationService.FindById(id);

            return new[] { account };
        }

    }
}