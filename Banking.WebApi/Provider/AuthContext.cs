using Microsoft.AspNet.Identity.EntityFramework;

namespace Banking.WebApi.Provider
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}