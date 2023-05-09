using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Portal.Application.Contracts;
using Portal.Application.Errors_Handler_Helper.Custom_Exceptions;
using Portal.Application.Helper;
using Portal.Application.Helper.Extensions;
using Portal.Application.Specifications.Attachment_Specifications;
using Portal.Application.Specifications.Ticket_Specifications;
using Portal.Domains.Entities;
using Portal.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Commends.CreateTicketAttachemnts
{
    public class CreateTicketAttachemuntsCommandHandler : IRequestHandler<CreateTicketAttachemuntsCommand, CreateTicketAttachmentTypeResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateTicketAttachemuntsCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<CreateTicketAttachmentTypeResponseDto> Handle(CreateTicketAttachemuntsCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userManager.GetUserIdAsyncExtension(request.Claims);

            var ticketSpec = new TicketForUserByAccountIdSpecifications(request.attachmentRequestDto.TicketId, userId);
            var ticket = await _unitOfWork.Repository<Ticket>().GetByIdWithSpecificationsAsync(ticketSpec);
            if (ticket == null)
                throw new ApiErrorResponse(404, "This Ticket Is Not Exist.");

            var ticketAttachment = await _unitOfWork.Repository<TicketAttachment>().GetByIdAsync(request.attachmentRequestDto.Id) ?? new TicketAttachment();
            if (request.attachmentRequestDto.Id > 0 && ticketAttachment == null)
                throw new ApiErrorResponse(404, "This Attachment Not Exist");
            _mapper.Map(request.attachmentRequestDto, ticketAttachment);

            if (request.attachmentRequestDto.Id != 0 && ticketAttachment.ImageUrl != null)
            {
                FileUploader.RemoveFile(FoldersNamesEnum.imgs, FoldersNamesEnum.attachments.ToString(), ticketAttachment.ImageUrl!);
            }


            if (request.attachmentRequestDto.File != null)
                ticketAttachment.ImageUrl = FileUploader.UploadFile(FoldersNamesEnum.imgs, FoldersNamesEnum.attachments.ToString(), request.attachmentRequestDto.File);

            if (request.attachmentRequestDto.Id == 0)
                await _unitOfWork.Repository<TicketAttachment>().AddAsync(ticketAttachment);
            else
                _unitOfWork.Repository<TicketAttachment>().Update(ticketAttachment);
            if (await _unitOfWork.Complete() <= 0)
                return new CreateTicketAttachmentTypeResponseDto()
                {
                    IsSuccess = false,
                    Message = "Error Happen"
                };

            return new CreateTicketAttachmentTypeResponseDto()
            {
                IsSuccess = true,
                Message = "Attachments Updated Successfully."
            };
        }
    }
}
