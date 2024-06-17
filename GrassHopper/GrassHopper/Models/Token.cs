using System.ComponentModel.DataAnnotations;

namespace GrassHopper.Models
{
    public class Token
    {
        [Key]
        public int TokenID { get; set; }

        public string TokenString { get; set; }

        public string TokenLength { get; set; }

        public string TokenType { get; set; }

        public DateTime CreationTime { get; set; }

    }
}
