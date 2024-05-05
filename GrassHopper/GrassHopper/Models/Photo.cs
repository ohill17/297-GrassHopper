using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GrassHopper.Models
{
    public class Photo //This is the model for photos in the data layer, it is not for uploading or display
    {
        [Key]
        public int PhotoId { get; set; }
        //Photos are stored as files, not on the DB
        //'Photos' in the DB contain a string for their unique file name, which can easily be formatted into the URL for the photo
        public string PhotoCode { get; set; } = null!;
        public string Extension { get; set; } = null!;
        public string PhotoName { get; set; } = null!;
        public string PhotoDescription { get; set; } = null!;
        public bool IsHidden { get; set; } = false;
        public PhotoGroup? Group { get; set; }
    }
}
