using System.ComponentModel.DataAnnotations;

namespace GrassHopper.Models
{
    public class AppSettings
    {
        [Key]
        public string FacebookAppId { get; set; }
        public string FacebookAppSecret { get; set; }
        public string FacebookRedirectUri { get; set; }
    }
}
