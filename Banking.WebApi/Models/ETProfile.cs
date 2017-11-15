using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banking.WebApi.Models
{
    public class EtProfile
    {
        public int ProfileId { get; set; }
        public string IdGuidAspNetUsers { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public string IdClienteSeguro { get; set; }
    }
}