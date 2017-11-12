using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using Banking.Domain.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking.Infrastructure.Repositories.EntityFramework;


namespace Banking.Infrastructure.Migrations
{

    public class BankingContext : DbContext
    {

        public BankingContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Operation> Operations { get; set; }


        public void RunMigrations()
        {
            using (var context = new BankingContext(Funciones.GetConnectionString()))
            {
                var customers = from c in context.Customers
                                select c;

                if (customers.Any()) return;
                addOperations(context);
                addCustomers(context);
                addBankAccoutns(context);
            }
        }



        void addOperations(BankingContext context)
        {
            var Operations = from c in context.Operations
                        select c;

            if (Operations.Count() == 0)
            {
                context.Operations.Add(new Operation { Name = "Transferencia" });
                context.SaveChanges();
            }
          
        }

        void addCustomers(BankingContext context)
        {
            context.Customers.Add(new Customer { Dni = "00132652", FirstName = "Cesar", LastName = "Paredes", Email = "guillermo.as007@gmail.com", Phone = "994883037", Password = Funciones.GetPassword(8) });
            context.Customers.Add(new Customer { Dni = "00152249", FirstName = "Erick", LastName = "Esquivel", Email = "jeffrey9113@gmail.com", Phone = "987009041", Password = Funciones.GetPassword(8) });
            context.Customers.Add(new Customer { Dni = "00136621", FirstName = "Omar", LastName = "Adrianzen", Email = "renzo.caballero26@gmail.com", Phone = "940547032", Password = Funciones.GetPassword(8) });
            context.Customers.Add(new Customer { Dni = "00130667", FirstName = "Rodrigo", LastName = "Cuba", Email = "cancer_19566@hotmail.com", Phone = "940547032", Password = Funciones.GetPassword(8) });
            context.Customers.Add(new Customer { Dni = "00134612", FirstName = "Rolando", LastName = "Felix", Email = "jgv_963@hotmail.com", Phone = "961000904", Password = Funciones.GetPassword(8) });
            context.Customers.Add(new Customer { Dni = "00133656", FirstName = "Kevin", LastName = "Ontaneda", Email = "alvin_20_66@hotmail.com", Phone = "979738935", Password = Funciones.GetPassword(8) });
            context.Customers.Add(new Customer { Dni = "00128624", FirstName = "Giancarlo", LastName = "Pujulla", Email = "juanmerchant07@gmail.com", Phone = "997202764", Password = Funciones.GetPassword(8) });
            context.Customers.Add(new Customer { Dni = "00135677", FirstName = "Alejandro", LastName = "Camarena", Email = "vicad16@gmail.com", Phone = "997071343", Password = Funciones.GetPassword(8) });
            context.Customers.Add(new Customer { Dni = "00140636", FirstName = "Adrian", LastName = "Talavera", Email = "cesarbsotomayor2@gmail.com", Phone = "987416998", Password = Funciones.GetPassword(8) });
            context.Customers.Add(new Customer { Dni = "00128648", FirstName = "Pierina", LastName = "Castro", Email = "jorge.alarcon@gmail.com", Phone = "942535619", Password = Funciones.GetPassword(8) });
            context.SaveChanges();
        }

        void addBankAccoutns(BankingContext context)
        {

            var customers = from c in context.Customers
                            select c;      

                List<BankAccount> accounts = new List<BankAccount>();
                foreach (Customer c in customers)
                {
                    BankAccount account = new BankAccount { CustomerId = c.CustomerId, Number = Funciones.CreateUnique16DigitString(), Balance = 1000, IsLocked = false };
                    BankAccount account2 = new BankAccount { CustomerId = c.CustomerId, Number = Funciones.CreateUnique16DigitString(), Balance = 1000, IsLocked = false };
                    BankAccount account3 = new BankAccount { CustomerId = c.CustomerId, Number = Funciones.CreateUnique16DigitString(), Balance = 1000, IsLocked = false };
                  
                    accounts.Add(account);
                    accounts.Add(account2);
                    accounts.Add(account3);
                }

                foreach (BankAccount a in accounts)
                {
                 context.BankAccounts.Add(a);
                }

                context.SaveChanges();
        }


    }


    public class Customer
    {
        public Customer()
        {

        }
        public virtual int CustomerId { get; set; }
        public virtual string Dni { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Password { get; set; }
        public virtual ICollection<BankAccount> bankAccounts { get; set; }
    }


    public class BankAccount
    {
        public BankAccount()
        {
        }
        public virtual int BankAccountId { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual string Number { get; set; }
        public virtual decimal? Balance { get; set; }
        public virtual bool? IsLocked { get; set; }
        public virtual Customer customer { get; set; }
    }


    public class Operation
    {
        public Operation()
        {
        }
        public virtual int OperationId { get; set; }
        public virtual string Name { get; set; }
    }


    public class Movement
    {
        public Movement()
        {
        }
        public virtual int MovementId { get; set; }
        public virtual int BankAccountId { get; set; }
        public virtual int OperationId { get; set; }
        public virtual DateTime date { get; set; }
        public virtual string NumberOriginBankAccount { get; set; }
        public virtual string NumberdestinationBankAccount { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public virtual Operation operation { get; set; }
    }


}
