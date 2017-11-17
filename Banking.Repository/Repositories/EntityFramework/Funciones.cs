using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Banking.Infrastructure.Repositories.EntityFramework
{
    public class Funciones
    {

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString;
        }

        public static string GetPassword(Int16 length = 8)
        {
            string guidResult = System.Guid.NewGuid().ToString();
            guidResult = guidResult.Replace("-", string.Empty);
            if (length <= 0 || length > guidResult.Length)
            {
                return guidResult;
            }
            else
            {
                return guidResult.Substring(0, length);
            }

        }


        private static Random RNG = new Random();

        public static string Create16DigitString()
        {
            var builder = new StringBuilder();
            while (builder.Length < 16)
            {
                builder.Append(RNG.Next(10).ToString());
            }
            return builder.ToString();
        }

        private static HashSet<string> Results = new HashSet<string>();

        public static string CreateUnique16DigitString()
        {
            var result = Create16DigitString();
            while (!Results.Add(result))
            {
                result = Create16DigitString();
            }

            return result;
        }


    }
}