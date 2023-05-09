using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.TicketAttachemnts.Commends.CreateTicketAttachemnts
{
    public class RequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public int TicketId { get; set; }
    }
}
