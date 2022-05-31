using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Models;

namespace TicketSystem.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                SeedDB(context, "0");
            }
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
