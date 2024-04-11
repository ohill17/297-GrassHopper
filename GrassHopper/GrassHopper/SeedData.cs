using GrassHopper.Data;
using GrassHopper.Models;
using Microsoft.AspNetCore.Identity;

namespace GrassHopper
{
    public class SeedData
    {
        public static void Seed(AppDbContext context)
        {
            Review review1 = new Review
            {
                ReviewID = 1,
                ReviewerName = "John Doe",
                ReviewBody = "Grasshopper General Construction did a great job on my bathroom remodel!",
                ReviewRating = 5,
                //ReviewDate = DateOnly.Parse("02/27/2006")
            };
        }
    }
}
