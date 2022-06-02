using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Authorization;
using TicketSystem.Data;
using TicketSystem.Enums;
using TicketSystem.Models;

namespace TicketSystem.Pages.Tickets
{
    public class DetailsModel : DI_BasePageModel
    {
        private readonly TicketSystem.Data.ApplicationDbContext _context;

        public DetailsModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        public Ticket Ticket { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Ticket = await Context.Ticket.FirstOrDefaultAsync(m => m.TicketId == id);

            if (Ticket == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.TicketManagersRole) ||
                               User.IsInRole(Constants.TicketAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != Ticket.OwnerID
                && Ticket.AuthStatus != AuthorizationStatus.Approved)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, AuthorizationStatus status)
        {
            var Ticket = await Context.Ticket.FirstOrDefaultAsync(
                                                      m => m.TicketId == id);

            if (Ticket == null)
            {
                return NotFound();
            }

            var TicketOperation = (status == AuthorizationStatus.Approved)
                                                       ? TicketOperations.Approve
                                                       : TicketOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Ticket,
                                        TicketOperation);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            Ticket.AuthStatus = status;
            Context.Ticket.Update(Ticket);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
