using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Pages.Tickets
{
    public class DetailsModel : PageModel
    {
        private readonly TicketSystem.Data.ApplicationDbContext _context;

        public DetailsModel(TicketSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket = await _context.Ticket.FirstOrDefaultAsync(m => m.TicketId == id);

            if (Ticket == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
