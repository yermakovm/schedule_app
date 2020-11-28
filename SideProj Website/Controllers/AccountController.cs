using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Entity.Entities.Auth;
using ScheduleApp.Dto;

namespace ScheduleApp.Controllers
{
    public class AccountController : Controller
    {
        private List<User> people = new List<User>
{
new User {UserName= "admin@gmail.com", Password="12345", Role = "admin" },
new User { UserName="qwerty@gmail.com", Password="55555", Role = "user" }
};

        [HttpPost("/token")]
        public IActionResult Token([FromBody] UserDto user)
        {
            var identity = GetIdentity(user);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }

        private ClaimsIdentity GetIdentity(UserDto user)
        {
            User person = people.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (person != null)
            {
                var claims = new List<Claim>
{
new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserName),
new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
};
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}