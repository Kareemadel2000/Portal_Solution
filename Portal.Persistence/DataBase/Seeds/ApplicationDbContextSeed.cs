using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portal.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Persistence.DataBase.Seeds
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if(roleManager.Roles.Count() <= 0) 
            {
                await roleManager.CreateAsync(new IdentityRole {Id = Guid.NewGuid().ToString(), Name = Roles.SuperAdmin.ToString(), NormalizedName= Roles.SuperAdmin.ToString().ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() });
                await roleManager.CreateAsync(new IdentityRole {Id = Guid.NewGuid().ToString(), Name = Roles.Admin.ToString(), NormalizedName= Roles.Admin.ToString().ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() });
                await roleManager.CreateAsync(new IdentityRole {Id = Guid.NewGuid().ToString(), Name = Roles.Client.ToString(), NormalizedName= Roles.Client.ToString().ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            }
        }
        public static async Task SeedAdmin(UserManager<IdentityUser> userManager)
        {
            if (!await userManager.Users.AnyAsync())
            {
                IdentityUser user = new IdentityUser
                {
                    Email = "SuperAdmin@SuperAdmin.com",
                    UserName = "SuperAdmin",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, "SuperAdmin@123");
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString());
                }
            }
        }
    }
}
