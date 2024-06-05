using System.ComponentModel.DataAnnotations.Schema;

namespace GrassHopper.Models
{
    [NotMapped]
    public class PhotoVM //This is the model for displaying a photo, it is not for database storage or uploading
    {
        public PhotoVM(Photo p, PhotoSize size = PhotoSize.Medium)
        {
            string sizeExtension;
            switch(size)
            {
                case PhotoSize.Small:
                    sizeExtension = "SM";
                    break;
                case PhotoSize.Medium:
                    sizeExtension = "MD";
                    break;
                case PhotoSize.Large:
                    sizeExtension = "LG";
                    break;
                default:
                    sizeExtension = "MD";
                    break;
            }
            PhotoId = p.PhotoId;
            Url = p.PhotoCode + sizeExtension + p.Extension;
            Name = p.PhotoName;
            Description = p.PhotoDescription;
            Group = p.Group;
        }
        public int PhotoId { get; set; }
        public string Url { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public PhotoGroup? Group { get; set; }
    }

    public enum PhotoSize
    {
        Small, 
        Medium, 
        Large
    }
}
