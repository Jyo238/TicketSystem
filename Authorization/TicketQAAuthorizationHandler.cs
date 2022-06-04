using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TicketSystem.Authorization;
using TicketSystem.Models;

namespace TicketManager.Authorization
{
    public class TicketQAAuthorizationHandler
                : AuthorizationHandler<OperationAuthorizationRequirement, Ticket>
    {

        public TicketQAAuthorizationHandler()
        {
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Ticket resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // QA can create/read/update/delete bugs.
            if (context.User.IsInRole(Constants.TicketQAsRole))
            {
                
                if (requirement.Name != Constants.CreateOperationName &&
                    requirement.Name != Constants.ReadOperationName &&
                    requirement.Name != Constants.UpdateOperationName &&
                    requirement.Name != Constants.DeleteOperationName)
                {
                    return Task.CompletedTask;
                }
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}