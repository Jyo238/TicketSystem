using System.ComponentModel.DataAnnotations;
using TicketSystem.Enums;

namespace TicketSystem.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string Title { get; set; }
        // user ID from AspNetUser table.
        public string OwnerID { get; set; }
        public TicketType Type { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Description { get; set; }

        public AuthorizationStatus Status { get; set; }
    }
}
