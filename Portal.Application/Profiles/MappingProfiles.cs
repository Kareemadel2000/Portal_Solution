using AutoMapper;
using Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Commands.CreateTicket;
using Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Queries;
using Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketTypes_Client.Queries;
using Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Commends.CreateTicketAttachemnts;
using Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Queries.GetAllTicketAttachments;
using Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.CreateTicketType;
using Portal.Application.Errors_Handler_Helper.Features.TicketType.Commands.UpdateTicketType;
using Portal.Application.Errors_Handler_Helper.Features.TicketType.Queries.GetAllTicketTypes;
using Portal.Application.ViewModels;
using Portal.Domains.Entities;
using Portal.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // ticket Types
            CreateMap<TicketType, GetAllTicketTypesResponseDto>();
            CreateMap<CreateTicketTypeCommand, TicketType>();
            CreateMap<CreateTicketTypeCommand, GetAllTicketTypesResponseDto>();
            CreateMap<UpdateTicketTypeCommand, TicketType>();
            CreateMap<UpdateTicketTypeCommand, UpdateTicketTypeCommandResponseDto>();
            CreateMap<TicketType, GenericTicketTypeResponseDto>();

            //Ticket
            CreateMap<CreateTicketRequestDto, Ticket>();
            CreateMap<UpdateTicketTypeCommandResponseDto, Ticket>();
            CreateMap<Ticket, GeneralTicketQueryResponseDto>().ForMember(x => x.TypeName,
                d => d.MapFrom(s => s.TicketType.TypeName));
            CreateMap<GetAllTicketAttachmentsResponseDto, TicketAttachment>().ReverseMap();

            //ticket Attachment
            CreateMap<CreateTicketAttachmentRequestDto, TicketAttachment>();

            //

            CreateMap<Ticket, TicketWithAttachmentViewModel>().ReverseMap();
            CreateMap<TicketType, TicketTypeVM>();
        }
    }
}
