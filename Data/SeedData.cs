using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Authorization;
using TicketSystem.Enums;
using TicketSystem.Models;

namespace TicketSystem.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            // For sample purposes seed both with the same password.
            // Password is set with the following:
            // dotnet user-secrets set SeedUserPW <pw>
            // The admin user can do anything

            var admin = await EnsureUser(serviceProvider, testUserPw, "admin@remotes.com");
            await EnsureRole(serviceProvider, admin, Constants.TicketAdministratorsRole);

            // allowed user can create and edit contacts that they create
            var rD = await EnsureUser(serviceProvider, testUserPw, "RD@remotes.com");
            await EnsureRole(serviceProvider, rD, Constants.TicketRDsRole);

            SeedDB(context, admin);
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Ticket.Any())
            {
                return;   // DB has been seeded
            }

            context.Ticket.AddRange(
                new Ticket
                {
                    Title = "Bug one",
                    Summary = "1234",
                    Description = "好多bug",
                    Status = AuthorizationStatus.Resolved,
                    OwnerID = adminID
                },
               new Ticket
               {
                   Title = "Feautre one",
                   Summary = "跟youtube一樣就好",
                   Description = "自動撥影片",
               },
               new Ticket
               {
                   Title = "Bug two",
                   Summary = "2345",
                   Description = "bug two",
               },
               new Ticket
               {
                   Title = "Feautre two",
                   Summary = "test",
                   Description = "bug two",
               });
            context.SaveChanges();
        }

    }
}
