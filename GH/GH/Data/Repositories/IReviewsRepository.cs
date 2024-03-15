using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using GH.Models;

namespace GH.Data.Repositories
{
    public interface IReviewsRepository
    {
        public List<ReviewModel> GetReviews();
        public Task<ReviewModel> GetReviewsByIdAsync(int id);
        public Task<int> StoreReviewsAsync(ReviewModel model);
        public int UpdateReviews(ReviewModel model);
        public int DeleteReviews(int id);
    }

    public class ReviewsRepository : IReviewsRepository
    {
        AppDbContext context;
        public ReviewsRepository(AppDbContext c)
        {
            context = c;
        }

        public List<ReviewModel> GetReviews()
        {
            return context.Reviews.ToList();
        }

        public async Task<ReviewModel> GetReviewsByIdAsync(int id)
        {
            var review = await context.Reviews.FindAsync(id);
            return review;
        }

        public async Task<int> StoreReviewsAsync(ReviewModel model)
        {
            await context.AddAsync(model);
            return await context.SaveChangesAsync();
        }

        public int UpdateReviews(ReviewModel model)
        {
            context.Reviews.Update(model);
            return context.SaveChanges();
        }

        public int DeleteReviews(int modelId)
        {
            ReviewModel review = GetReviewsByIdAsync(modelId).Result;
            context.Reviews.Remove(review);
            return context.SaveChanges();
        }
    }
}
