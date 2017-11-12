using System;
using Banking.Application;
using Banking.Application.Dto;
using System.Web.Http;

namespace Banking.WebApi.Controllers
{
    public class PerformTransferController : ApiController
    {

        //http://localhost:10559/api/PerformTransfer?accountFrom=5&accountTo=1&amount=1
        //GET: /PerformTransfer/4767421619142000/0523218924860120/100

        [HttpGet]
        public ResultDto Perform(string accountFrom, string accountTo, decimal amount)
        {
            try
            {
                var bankingApplicationService = new BankingApplicationService();
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

    }
}