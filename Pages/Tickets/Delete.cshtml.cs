using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Authorization;
using TicketSystem.Data;
using TicketSystem.Models;


namespace TicketSystem.Pages.Tickets
{
    public class DeleteModel : DI_BasePageModel
    {
        private readonly TicketSystem.Data.ApplicationDbContext _context;

        public DeleteModel(
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
                                                     TicketOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var Ticket = await Context
                .Ticket.AsNoTracking()
                .FirstOrDefaultAsync(m => m.TicketId == id);

            if (Ticket == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Ticket,
                                                     TicketOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Ticket.Remove(Ticket);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
