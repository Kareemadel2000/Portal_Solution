using MediatR;
using Microsoft.AspNetCore.Identity;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;
using Portal.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.Clients_Accounts.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly UserManager<IdentityUser> _userManager;
        public RegisterCommandHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                throw new ApiErrorResponse(400, "Email Not Valid.");
            if (await _userManager.FindByNameAsync(request.Username) != null)
                throw new ApiErrorResponse(400, "Username Not Valid.");

            var user = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                if (result.Succeeded)
                {
                    return new RegisterResponseDto
                    {
                        IsSuccess = true,
                        Message = "Register Success."
                    };
                }
            }

            throw new InvalidRequestException(result.Errors.Select(x => x.Description).ToList());
        }
    }
}
