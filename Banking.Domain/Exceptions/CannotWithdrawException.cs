using System;

namespace Banking.Domain.Exceptions
{
    public class CannotWithdrawException : Exception
    {
        public CannotWithdrawException() :
            base("Cannot withdraw in the account, it is locked or amount is greater than balance")
        {
        }
    }
}
