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
        //Photos will be stored as files, not on the DB
        //'Photos' in the DB will contain a string for their unique file name, which can easily be formatted into the URL for the photo
        public string PhotoCode { get; set; }
        public string PhotoName { get; set; }
        public string PhotoDescription { get; set; }
    }
}