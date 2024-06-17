using GrassHopper.Models;

namespace GrassHopper.Data.Repositories
{
    public interface IReviewRepository
    {
        public Task<Review> GetReview(int id);
        public Task<List<Review>> GetAllReviews();
        public Task<int> AddReview(Review photo);
        public Task<int> UpdateReview(Review photo);
        public Task<int> DeleteReview(int id);
    }
}
