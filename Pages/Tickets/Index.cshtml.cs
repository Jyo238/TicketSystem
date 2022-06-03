using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Authorization;
using TicketSystem.Data;
using TicketSystem.Enums;
using TicketSystem.Models;

namespace TicketSystem.Pages.Tickets
{
    public class IndexModel : DI_BasePageModel
    {
        private readonly TicketSystem.Data.ApplicationDbContext _context;
        public IndexModel(
                ApplicationDbContext context,
                IAuthorizationService authorizationService,
                UserManager<IdentityUser> userManager)
                : base(context, authorizationService, userManager)
        {
        }

        public IList<Ticket> Ticket { get;set; }

        public async Task OnGetAsync()
        {
            var Tickets = from c in Context.Ticket
                           select c;

            var isAuthorized = User.IsInRole(Constants.TicketRDsRole) ||
                               User.IsInRole(Constants.TicketAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only Resolved Tickets are shown UNLESS you're authorized to see them
            // or you are the owner.
            //if (!isAuthorized)
            //{
            //    Tickets = Tickets.Where(c => c.Status == AuthorizationStatus.Resolved
            //                                || c.OwnerID == currentUserId);
            //}

            Ticket = await Tickets.ToListAsync();
        }
    }
}
