using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Infrastructure.Repositories
{
    class Functions
    {
       public static string GetConnectionString()
       {
           return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
       }


    }
}
