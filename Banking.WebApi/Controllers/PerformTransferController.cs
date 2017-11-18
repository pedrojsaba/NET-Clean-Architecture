using System;
using System.Web.Http;
using Banking.Application;
using Banking.Application.Dto;
using Microsoft.AspNet.Identity;

namespace Banking.WebApi.Controllers
{
    [RoutePrefix("api/PerformTransfer")]
    public class PerformTransferController : ApiController
    {

        private void ValidatePerform(string accountFrom, string accountTo, decimal amount)
        {
            var bankingApplicationService = new BankingApplicationService();            

            if (!bankingApplicationService.AccountEnabled(accountFrom)) throw new Exception("La Cuenta de Origen no es valida.");
            if (!bankingApplicationService.AccountEnabled(accountTo)) throw new Exception("La Cuenta de Destino no es valida.");


            var id = User.Identity.GetUserName();
            if (!bankingApplicationService.OwnAccount(id, accountTo)) throw new Exception("La Cuenta de Origen no te pertenece.");

            if (accountFrom.Equals(accountTo)) throw new Exception("No se puede transferir a la misma cuenta.");

            if (amount == 0) throw new Exception("No se puede transferir este monto.");

            if (amount < 0) throw new Exception("No se puede transferir montos negativos.");

            if (bankingApplicationService.InsufficientBalance(accountFrom, amount)) throw new Exception("Saldo Insuficiente.");
        }

        //GET: /PerformTransfer/4767421619142000/0523218924860120/100

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public ResultDto Perform(string accountFrom, string accountTo, decimal amount)
        {
            try
            {
                var bankingApplicationService = new BankingApplicationService();

                ValidatePerform(accountFrom, accountTo, amount);

                bankingApplicationService.PerformTransfer(new BankAccountDto { Number = accountFrom }, new BankAccountDto { Number = accountTo }, amount);
                return new ResultDto
                {
                    Status = "OK",
                    Message = string.Empty
                };
            }
            catch (Exception exception)
            {
                return new ResultDto
                {
                    Status = "ERROR",
                    Message = exception.Message
                };
            }

        }

        // GET api/PerformTransfer/AccountEnabled     
        [HttpGet]
        //[AllowAnonymous]
        [Authorize(Roles = "administrator")]
        public bool AccountEnabled(string id)
        {
            return new BankingApplicationService().AccountEnabled(id);
        }
    }
}