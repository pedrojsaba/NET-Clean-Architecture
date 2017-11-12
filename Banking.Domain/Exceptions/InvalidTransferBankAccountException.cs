using System;

namespace Banking.Domain.Exceptions
{
public class InvalidTransferBankAccountException : Exception {
    public InvalidTransferBankAccountException():
        base("Cannot perform the transfer, invalid data in bank accounts specifications"){
    }
}
}
