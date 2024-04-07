namespace GrassHopper.Models
{
    //DEPRECATED, DO NOT USE THIS
    public class PhotoUploadModel
    {
        public IFormFile file { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoName { get; set; }
        public string PhotoDescription { get; set; }
    }
}
