using Banking.Application.Dto;
using Banking.Domain.Repositories;
using Banking.Domain.Services;
using System.Transactions;
using Banking.Infrastructure.Repositories.EntityFramework;
using System.Collections.Generic;


namespace Banking.Application
{

    public class BankingApplicationService
    {

        private readonly IBankAccountRepository _bankAccountRepository = new BankAccountRepositoryEF();
        private readonly TransferDomainService _transferDomainService = new TransferDomainService();
        private readonly ICustomerRepository _customerRepository = new CustomerRepositoryEF();


        public void PerformTransfer(BankAccountDto originBankAccountDto, BankAccountDto destinationBankAccountDto, decimal amount)
        {
            var scope = new TransactionScope();

            using (scope)
            {
                var originAccount = _bankAccountRepository.FindByNumber(originBankAccountDto.Number);
                var destinationAccount = _bankAccountRepository.FindByNumber(destinationBankAccountDto.Number);
                _transferDomainService.PerformTransfer(originAccount, destinationAccount, amount);
                _bankAccountRepository.update(originAccount);
                _bankAccountRepository.update(destinationAccount);
                scope.Complete();
            }
        }

        public Domain.Model.BankAccount FindByNumber(string number)
        {
            return _bankAccountRepository.FindByNumber(number);
        }

        public List<Domain.Model.Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Domain.Model.Customer GetByCustomerId(int customerId)
        {
            return _customerRepository.GetByCustomerId(customerId);
        }

        public List<Domain.Model.BankAccount> GetAccounts(int customerId)
        {
            return _bankAccountRepository.GetByCustomerId(customerId);
        }

        public bool AccountEnabled(string accountNumber)
        {
            return _bankAccountRepository.AccountEnabled(accountNumber);
        }

        public bool OwnAccount(string username,string accountNumber)
        {
            return _bankAccountRepository.OwnAccount(username,accountNumber);
        }
        public bool InsufficientBalance(string accountNumber, decimal amount)
        {
            return _bankAccountRepository.InsufficientBalance(accountNumber, amount);
        }
        public List<Domain.Model.BankAccount> GetAccounts(string username)
        {
            return _bankAccountRepository.GetByUsername(username);
        }

        public Domain.Model.BankAccount FindById(int bankAccountId)
        {
            return _bankAccountRepository.FindById(bankAccountId);
        }

    }
}
