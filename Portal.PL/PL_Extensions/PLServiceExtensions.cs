using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.SeedWork.Custom_Problem_Details;
using Portal.Persistence.DataBase;

namespace Portal.PL.PL_Extensions
{
    public static class PLServiceExtensions
    {
        public static IServiceCollection AddApiErrorsServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value?.Errors.Count > 0)
                                            .SelectMany(M => M.Value?.Errors!)
                                            .Select(E => E.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse(errors.ToList());
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            //Identity Configuration
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = new PathString("/Accont/Login");
                    options.AccessDeniedPath = new PathString("/Accont/Login");
                });

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider); 


            //// password and user name configuration 

            //services.AddIdentity<IdentityUser, IdentityRole>(options =>
            //{
            //    // Default Password settings.
            //    options.User.RequireUniqueEmail = true;
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequiredUniqueChars = 0;
            //}).AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}
