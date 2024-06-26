﻿using GrassHopper.Models;
using GrassHopper.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GrassHopper.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private AppDbContext dbContext;
        
        public ReviewRepository(AppDbContext dbCntxt)
        {
            dbContext = dbCntxt;
        }
        public async Task<int> AddReview(Review review)
        {
            await dbContext.Reviews.AddAsync(review);
            return dbContext.SaveChanges();
        }

        public async Task<int> DeleteReview(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Review>> GetAllReviews()
        {
            return await dbContext.Reviews.ToListAsync();
        }

        public async Task<Review> GetReview(int id)
        {
            return await dbContext.Reviews.FindAsync(id);
        }

        public async Task<int> UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
