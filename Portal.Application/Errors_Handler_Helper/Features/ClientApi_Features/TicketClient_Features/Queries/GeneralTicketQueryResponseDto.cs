﻿using Portal.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Errors_Handler_Helper.Features.ClientApi_Features.TicketClient_Features.Queries
{
    public class GeneralTicketQueryResponseDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public DateTime CreationAt { get; set; }
        public int TicketTypeId { get; set; }
        public string TypeName { get; set; }
    }
}
