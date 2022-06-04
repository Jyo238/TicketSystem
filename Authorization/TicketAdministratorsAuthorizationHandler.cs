using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TicketSystem.Authorization;
using TicketSystem.Models;

namespace TicketManager.Authorization
{
    public class TicketAdministratorsAuthorizationHandler
                    : AuthorizationHandler<OperationAuthorizationRequirement, Ticket>
    {
        protected override Task HandleRequirementAsync(
                                    AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                    Ticket resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.TicketAdministratorsRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}