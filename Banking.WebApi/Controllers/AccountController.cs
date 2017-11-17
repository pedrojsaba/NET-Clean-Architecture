using Banking.WebApi.Engine;
using Banking.WebApi.Models;
using Banking.WebApi.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

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


        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repo.RegisterUser(userModel);

            var errorResult = GetErrorResult(result);

            return errorResult != null ? new HttpActionResult(HttpStatusCode.InternalServerError, "Internal Server Error") : new HttpActionResult(HttpStatusCode.Created, "Successful");
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

            return errorResult != null ? new HttpActionResult(HttpStatusCode.InternalServerError, "Internal Server Error") : new HttpActionResult(HttpStatusCode.Created, "Successful");
        }

        // POST api/Account/GetAllRoles
        [AllowAnonymous]
        [Route("GetAllRoles")]
        public HttpResponseMessage GetAllRoles()
        {
            var listRolesStorage = new List<IdentityRole>(_repo.GetAllRoles());
            var listRolesStorageTotal = new List<IdentityRole>();

            foreach (var items in listRolesStorage)
            {
                var identity = new IdentityRole
                {
                    Id = items.Id,
                    Name = items.Name
                };
                listRolesStorageTotal.Add(identity);
            }

            var json = JsonConvert.SerializeObject(listRolesStorageTotal, Formatting.Indented);
            var answer = new HttpResponseMessage()
            {
                Content = new StringContent(json)
            };
            answer.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return answer;
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
