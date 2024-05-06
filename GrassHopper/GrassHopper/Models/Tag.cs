using System.ComponentModel.DataAnnotations;

namespace GrassHopper.Models
{
    public class Tag
    {
        [Key]
        public string TagText { get; set; } = null!;
    }
}
