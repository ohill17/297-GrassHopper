using System.ComponentModel.DataAnnotations;

namespace GrassHopper.Models
{
    public class Token
    {
        [Key]
        public int TokenID { get; set; }

        public string TokenString { get; set; }

        //public int TimeUntilInvalid { get; set; }

    }
}
