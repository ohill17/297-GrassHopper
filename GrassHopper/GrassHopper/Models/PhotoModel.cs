using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GrassHopper.Models
{
    public class PhotoModel
    {
        [Key]
        public int PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string PhotoDescription { get; set; }
        public byte[] Photo { get; set; }
    }
}
