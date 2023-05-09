using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Helper.Extensions
{
    public static class UserExtensions
    {
        public static async Task<string> GetUserIdAsyncExtension(this UserManager<IdentityUser> userManager, ClaimsPrincipal UserClaims)
        {
            var email = UserClaims.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            return user.Id;
        }
    }
}
