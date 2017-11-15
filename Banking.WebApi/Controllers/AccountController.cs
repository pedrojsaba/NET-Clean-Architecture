using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;
using Banking.WebApi.Models;
using Banking.WebApi.Repository;

namespace Banking.WebApi.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly AuthRepository _repo;

        public AccountController()
        {
            _repo = new AuthRepository();
        }


      

        // POST api/Account/Role
        [AllowAnonymous]
        [Route("Role")]
        public async Task<IHttpActionResult> AddRole(UserModelRole role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repo.AddRole(role);

            var errorResult = GetErrorResult(result);

            return errorResult ?? Ok();

            //return new HttpActionResult(HttpStatusCode.Created, "Successful");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Succeeded) return null;
            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();
            }

            return BadRequest(ModelState);
        }
    }
}