using System.ComponentModel.DataAnnotations;

namespace GrassHopper.Models
{
    public class Review
    {
        [Key]
       public string ReviewID { get; set; }
       public string ReviewerName { get; set; }
       public int ReviewBody { get; set; }
       public int ReviewRating {  get; set; }

    
    }
}
