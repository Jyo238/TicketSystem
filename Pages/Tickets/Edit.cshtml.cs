using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Authorization;
using TicketSystem.Data;
using TicketSystem.Enums;
using TicketSystem.Models;

namespace TicketSystem.Pages.Tickets
{
    public class EditModel : DI_BasePageModel
    {
        private readonly TicketSystem.Data.ApplicationDbContext _context;

        public EditModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Ticket = await Context.Ticket.FirstOrDefaultAsync(
                                                 m => m.TicketId == id);

            if (Ticket == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Ticket,
                                                      TicketOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch Ticket from DB to get OwnerID.
            var ticket = await Context
                .Ticket.AsNoTracking()
                .FirstOrDefaultAsync(m => m.TicketId == id);

            if (ticket == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, ticket,
                                                     TicketOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            ticket.OwnerID = Ticket.OwnerID;

            Context.Attach(ticket).State = EntityState.Modified;

            ticket.Title = Ticket.Title;
            ticket.Description = Ticket.Description;
            ticket.Summary = Ticket.Summary;

            if (ticket.Status == AuthorizationStatus.Resolved)
            {
                // If the Ticket is updated after approval, 
                // and the user cannot Resolve,
                // set the status back to submitted so the update can be
                // checked and Resolved.
                var canResolve = await AuthorizationService.AuthorizeAsync(User,
                                        ticket,
                                        TicketOperations.Resolve);

                if (!canResolve.Succeeded)
                {
                    ticket.Status = AuthorizationStatus.Stateless;
                }
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.TicketId == id);
        }
    }
}
