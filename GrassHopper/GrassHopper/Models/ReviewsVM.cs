namespace GrassHopper.Models
{
    public class ReviewsVM
    {
        public string FacebookAppId { get; set; }
        public string FacebookAppSecret { get; set; }
        public string FacebookRedirectUri { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
