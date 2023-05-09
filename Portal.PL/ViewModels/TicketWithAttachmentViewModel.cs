using Portal.Domains.Entities;

namespace Portal.Application.ViewModels
{
    public class TicketWithAttachmentViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }                
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public int TicketTypeId { get; set; }        
        public List<string> AttachmentImageUrl { get; set; } = new List<string>();
    }
}
