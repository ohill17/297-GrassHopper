using System.ComponentModel.DataAnnotations;

namespace GrassHopper.Models
{
    public class Review
    {
        [Key]
       public int ReviewID { get; set; }
       public string ReviewerName { get; set; }
       public string ReviewBody { get; set; }
       public int ReviewRating {  get; set; }
       public DateOnly Date { get; set; }
    
    }
}
