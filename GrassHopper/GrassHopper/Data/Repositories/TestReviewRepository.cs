using GrassHopper.Models;

namespace GrassHopper.Data.Repositories
{
    public class TestReviewRepository : IReviewRepository
    {
        private List<Review> reviews = new();
        public async Task<int> AddReview(Review rev)
        {
            rev.ReviewID = reviews.Count;
            reviews.Add(rev);
            return reviews.IndexOf(rev);
        }

        public async Task<int> DeleteReview(int id)
        {
            reviews.RemoveAt(id);
            return reviews.Count;
        }

        public async Task<List<Review>> GetAllReviews()
        {
            return reviews;
        }

        public async Task<Review> GetReview(int id)
        {
            return reviews.Where(r => r.ReviewID == id).FirstOrDefault();
        }

        public async Task<int> UpdateReview(Review rev)
        {
            throw new NotImplementedException(); //Updating this stuff is clunky and awkward in a testing repo so I won't bother
        }
    }
}
