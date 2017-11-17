using Banking.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.WebApi.Engine;
using Banking.WebApi.Provider;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Banking.WebApi.Repository
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;
        private UserManager<IdentityUser> _userManager;
        private DLProfile dLprofile;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email
            };

            UserModelRole modelRoles = new UserModelRole
            {
                Role = userModel.Role
            };

            //Register rol
            await AddRole(modelRoles);

            //Register User
            var result = await _userManager.CreateAsync(user, userModel.Password);

            //Register of user in rol
            await AddUserInRole(user.Id, userModel);

            //Register profile
            dLprofile = new DLProfile();
            ETProfile eProfile = new ETProfile();
            eProfile.IdGuidAspNetUsers = user.Id;
            eProfile.FirstName = userModel.eProfile.FirstName;
            eProfile.LastName = userModel.eProfile.LastName;
            eProfile.Address = userModel.eProfile.Address;
            eProfile.Gender = userModel.eProfile.Gender;
            eProfile.Image = userModel.eProfile.Image;
            eProfile.IdClienteSeguro = userModel.eProfile.IdClienteSeguro;
            dLprofile.RegisterProfile(eProfile);

            return result;
        }
        public async Task<IdentityResult> DeleteUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName,
            };

            var result = await _userManager.DeleteAsync(user);

            return result;
        }
        public async Task<IdentityResult> AddRole(UserModelRole role)
        {
            var roleStore = new RoleStore<IdentityRole>(_ctx);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var result = await roleManager.CreateAsync(new IdentityRole { Name = role.Role });
            return result;
        }

        public List<IdentityRole> GetAllRoles()
        {
            var roleStore = new RoleStore<IdentityRole>(_ctx);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var result = roleManager.Roles.ToList();
            return result;
        }

        public async Task AddUserInRole(string Id, UserModel userModel)
        {
            await _userManager.AddToRoleAsync(Id, userModel.Role);
        }
        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }
        public async Task<IList<string>> FindUserInRole(string userId)
        {
            IList<string> user = await _userManager.GetRolesAsync(userId);
            return user;
        }

        public IDictionary<string, string> FindUserProfile(string userId)
        {
            DLProfile profile = new DLProfile();
            var p = profile.GetProfile(userId);

            IDictionary<string, string> profileList = new Dictionary<string, string>();
            profileList.Add("ProfileID", p.ProfileID.ToString());
            profileList.Add("IdGuidAspNetUsers", p.IdGuidAspNetUsers);
            profileList.Add("FirstName", p.FirstName);
            profileList.Add("LastName", p.LastName);
            profileList.Add("Address", p.Address);
            profileList.Add("Gender", p.Gender);
            profileList.Add("Image", p.Image);
            profileList.Add("IdClienteSeguro", p.IdClienteSeguro);

            return profileList;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}