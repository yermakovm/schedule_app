using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ScheduleApp.Database
{
    public class AuthRepository : IDisposable
    {
        private readonly AuthContext _ctx;

        private UserManager<IdentityUser> _userManager;

        public AuthRepository(AuthContext context, UserManager<IdentityUser> userManager)
        {
            _ctx = context;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser(User userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindByLoginAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}
