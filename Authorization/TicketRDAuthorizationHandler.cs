using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TicketSystem.Authorization;
using TicketSystem.Models;

namespace TicketManager.Authorization
{
    public class TicketRDAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Ticket>
    {
        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Ticket resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for approval/reject, return.
            //if (requirement.Name != Constants.ResolveOperationName &&
            //    requirement.Name != Constants.RejectOperationName)
            //{
            //    return Task.CompletedTask;
            //}

            // RD can Resolve or reject.
            if (context.User.IsInRole(Constants.TicketRDsRole))
            {
                //如果不是 Reslove / Reject相關的 直接return.
                if (requirement.Name != Constants.ResolveOperationName &&
                requirement.Name != Constants.RejectOperationName)
                {
                    return Task.CompletedTask;
                }

                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}