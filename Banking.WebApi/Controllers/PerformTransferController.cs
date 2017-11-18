using System;
using System.Web.Http;
using Banking.Application;
using Banking.Application.Dto;

namespace Banking.WebApi.Controllers
{
    [RoutePrefix("api/PerformTransfer")]
    public class PerformTransferController : ApiController
    {

        //GET: /PerformTransfer/4767421619142000/0523218924860120/100

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public ResultDto Perform(string accountFrom, string accountTo, decimal amount)
        {
            try
            {
                var bankingApplicationService = new BankingApplicationService();
                if (bankingApplicationService.InsufficientBalance(accountFrom, amount)) throw new Exception("Saldo Insuficiente.");

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