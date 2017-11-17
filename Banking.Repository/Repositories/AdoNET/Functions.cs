using System.Configuration;

namespace Banking.Infrastructure.Repositories
{
    class Functions
    {
       public static string GetConnectionString()
       {
           return ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString;
       }


    }
}
