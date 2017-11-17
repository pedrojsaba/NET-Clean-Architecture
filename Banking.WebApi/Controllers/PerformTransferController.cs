﻿using System;
using System.Web.Http;
using Banking.Application;
using Banking.Application.Dto;

namespace Banking.WebApi.Controllers
{
    [RoutePrefix("api/BankAccount")]
    public class PerformTransferController : ApiController
    {
        
        //GET: /PerformTransfer/4767421619142000/0523218924860120/100

        [HttpGet]
        [Authorize(Roles = "client")]
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