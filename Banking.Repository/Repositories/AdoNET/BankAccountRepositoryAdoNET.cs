using Banking.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Banking.Domain.Model;

// ReSharper disable once CheckNamespace
namespace Banking.Infrastructure.Repositories
{
    public class BankAccountRepositoryAdoNet : IBankAccountRepository
    {
        public BankAccount FindByNumber2(string accountNumber)
        {
            var bankAccount = new BankAccount();
            const string cmdText = "SELECT [BankAccountId],[number], [balance],[IsLocked],[CustomerId] FROM BankAccounts WHERE number = @account_number";
            using (var con = new SqlConnection(Functions.GetConnectionString()))
            {
                var command = new SqlCommand(cmdText, con) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[]
                {
                    new SqlParameter
                    {
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                        ParameterName = "@account_number",
                        Value = accountNumber
                    }
                });
                con.Open();

                using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        bankAccount.Id = reader.GetInt32(reader.GetOrdinal("BankAccountId"));
                        bankAccount.Number = reader.GetString(reader.GetOrdinal("number"));
                        bankAccount.Balance = reader.GetDecimal(reader.GetOrdinal("balance"));
                        bankAccount.IsLocked = reader.GetBoolean(reader.GetOrdinal("IsLocked"));
                    }
                }
            }
            return bankAccount;
        }


        public BankAccount FindByNumber(string accountNumber)
        {

            var bankAccount = new BankAccount();
            const string query = "SELECT [BankAccountId],[number], [balance],[IsLocked],[CustomerId] FROM BankAccounts WHERE number = @accountNumber";

            using (var conn = new SqlConnection(Functions.GetConnectionString()))
            {
                
                var command = new SqlCommand(query, conn) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[]
                {
                    new SqlParameter
                    {
                        SqlDbType = SqlDbType.VarChar,
                        ParameterName = "@accountNumber",
                        Value = accountNumber
                    }
                });

                conn.Open();

                using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        bankAccount.Id = (reader.GetInt32(reader.GetOrdinal("BankAccountId")));
                        bankAccount.Number = (reader.GetString(reader.GetOrdinal("number")));
                        bankAccount.Balance = (reader.GetDecimal(reader.GetOrdinal("balance")));
                        bankAccount.IsLocked = (reader.GetBoolean(reader.GetOrdinal("IsLocked")));
                    }
                }

            }

            return bankAccount;

        }
        public bool AccountEnabled(string accountNumber)
        {
            throw new NotImplementedException();
        }
        public List<BankAccount> GetByUsername(string username)
        {
            throw new NotImplementedException();
        }
        public bool InsufficientBalance(string accountNumber, decimal amount)
        {
            throw new NotImplementedException();
        }
        public BankAccount FindByNumberLocked(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public void save(BankAccount entity)
        {
            throw new NotImplementedException();
        }

        public void update(BankAccount entity)
        {
            throw new NotImplementedException();
        }

        public void merge(BankAccount entity)
        {
            throw new NotImplementedException();
        }


        public System.Collections.Generic.List<BankAccount> GetByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }


        public BankAccount FindById(int bankAccountId)
        {
            throw new NotImplementedException();
        }
    }
}
