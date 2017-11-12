using Banking.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using Jose;

namespace Banking.WebApi.Controllers
{
    public class TokenController : ApiController
    {

        [AllowAnonymous]
        public string Get(string username, string password)
        {
            var payload = new Dictionary<string, object>
            {
                {"sub", "mr.x@contoso.com"},
                {"exp", 1300819380}
            };

            var secretKey = Encoding.UTF8.GetBytes("myawesomekey");

            var token = JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
            return token;

        }

        public bool CheckUser(string username, string password)
        {
            // should check in the database
            return true;
        }

    }
}