using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TicketSystem.Authorization;
using TicketSystem.Models;

namespace TicketManager.Authorization
{
    public class TicketIsOwnerAuthorizationHandler
                : AuthorizationHandler<OperationAuthorizationRequirement, Ticket>
    {
        UserManager<IdentityUser> _userManager;

        public TicketIsOwnerAuthorizationHandler(UserManager<IdentityUser>
            userManager)
        {
            _userManager = userManager;
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

            // If not asking for CRUD permission, return.

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            //這邊是確定是否為只能看自己的單
            //if (resource.OwnerID == _userManager.GetUserId(context.User))
            //{
            //    context.Succeed(requirement);
            //}
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}