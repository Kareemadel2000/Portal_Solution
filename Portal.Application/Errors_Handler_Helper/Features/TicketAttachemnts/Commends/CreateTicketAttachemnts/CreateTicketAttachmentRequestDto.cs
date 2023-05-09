using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Commends.CreateTicketAttachemnts
{
    public class CreateTicketAttachmentRequestDto
    {
        public CreateTicketAttachmentRequestDto()
        {

        }
        public int Id { get; set; } = 0;        
        public IFormFile File { get; set; }        
        public int TicketId { get; set; }
    }
}
