using Banking.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Banking.Application.Dto;

namespace Banking.WebApi.Controllers
{
    public class TransferenciaController : ApiController
    {

        public  BankAccountDto Get(string id)
        {
            const string error = "No found";
            try
            {
                var bankingApplicationService = new BankingApplicationService();

                var account = bankingApplicationService.FindByNumber(id);
                if (account == null) throw new ArgumentNullException(error);

                return new BankAccountDto
                {
                    Balance = account.Balance,
                    Id = account.Id,
                    IsLocked = account.IsLocked,
                    Number = account.Number
                };
            }
            catch (Exception)
            {
                return new BankAccountDto();
            }
        }
    }
}