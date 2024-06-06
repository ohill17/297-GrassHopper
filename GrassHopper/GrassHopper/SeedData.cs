using GrassHopper.Data;
using GrassHopper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace GrassHopper
{
    public class SeedData
    {
        public static void Seed(IServiceProvider provider, Dictionary<string, string> adminInfo)
        {
            var context = provider.GetRequiredService<AppDbContext>();
            var userManager = provider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleManager.Roles.Any())
            {
                _ = roleManager.CreateAsync(new IdentityRole("Admin")).Result.Succeeded;
            }

            if (userManager.FindByNameAsync(adminInfo["AdminUName"]).Result is null)
            {
                AppUser admin = new()
                {
                    UserName = adminInfo["AdminUName"],
                    Name = adminInfo["AdminName"]
                };
                bool result = userManager.CreateAsync(admin, adminInfo["AdminPass"]).Result.Succeeded;
                if (result)
                {
                    _ = userManager.AddToRoleAsync(admin, "Admin").Result.Succeeded;
                }
            }

            if (!context.Reviews.Any())
            {
              Review review1 = new Review
                {
                    ReviewID = 1,
                    ReviewerName = "John Doe",
                    ReviewBody = "Grasshopper General Construction did a great job on my bathroom remodel!",
                    ReviewRating = 5,
                    ReviewDate = DateOnly.Parse("02/27/2006")
                };

                context.Reviews.Add(review1);
                context.SaveChanges();

                Review review2 = new Review
                {
                    ReviewID = 2,
                    ReviewerName = "Jane Toe",
                    ReviewBody = "Love my new spruce deck.",
                    ReviewRating = 5,
                    ReviewDate = DateOnly.Parse("02/27/2006")
                };

                context.Reviews.Add(review2);
                context.SaveChanges();
            }
        }
    }
}
