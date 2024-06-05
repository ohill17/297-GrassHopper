using System.ComponentModel.DataAnnotations.Schema;

namespace GrassHopper.Models
{
    [NotMapped]
    public class PhotoUploadVM //This is the model for uploading photos, it is not for database storage or for display
    {
        public IFormFile File { get; set; } = null!;
        public string PhotoName { get; set; } = null!;
        public string PhotoDescription { get; set; } = null!;
    }
}