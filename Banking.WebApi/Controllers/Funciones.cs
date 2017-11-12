using System.Configuration;

namespace Banking.WebApi.Controllers
{
    public class Funciones
    {
        public static string GetConnectionString()
        {
            return  ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

    }
}