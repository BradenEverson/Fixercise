using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RepDeck
{
    [Table("AspNetUsers")]
    public class User : IdentityUser
    {
        public string bindingString { get; set; }
        public User(String UserName, string Email)
        {
            this.UserName = UserName;
            this.Email = Email;
            this.bindingString = Guid.NewGuid().ToString().Split('-')[0];
        }
    }
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<User> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrWhiteSpace(user.bindingString))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
        new Claim(ClaimTypes.Authentication, user.bindingString)
    });
            }
            return principal;
        }
    }
}
