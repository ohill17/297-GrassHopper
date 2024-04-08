using System.ComponentModel.DataAnnotations.Schema;

namespace GrassHopper.Models
{
    [NotMapped]
    public class PhotoUploadVM
    {
        public IFormFile File { get; set; } = null!;
        public string PhotoName { get; set; } = null!;
        public string PhotoDescription { get; set; } = null!;
    }
}