using System.ComponentModel.DataAnnotations.Schema;

namespace GrassHopper.Models
{
    [NotMapped]
    public class GroupUploadVM
    {
        public List<IFormFile> Files { get; set; } = new();
        public string GroupName { get; set; } = null!;
        public string GroupDescription { get; set; } = null!;
    }
}
