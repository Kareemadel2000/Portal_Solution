using MediatR;
using Microsoft.AspNetCore.Identity;
using Portal.Application.Contracts.InterfacesForApi;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;
using Portal.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.Clients_Accounts.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenServices _tokenServices;
        public LoginCommandHandler(UserManager<IdentityUser> userManager, ITokenServices tokenServices)
        {
            _userManager = userManager;
            _tokenServices = tokenServices;
        }
        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Username) ?? await _userManager.FindByNameAsync(request.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new ApiErrorResponse(401, "Invalid Username Or Password");

            if (!await _userManager.IsInRoleAsync(user, Roles.Client.ToString()))
                throw new ApiErrorResponse(401, "Invalid Login");

            return new LoginResponseDto()
            {
                IsSuccess = true,
                Token = await _tokenServices.CreateToken(user, _userManager)
            };
        }
    }
}
