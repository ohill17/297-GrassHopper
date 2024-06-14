using System;
using System.ComponentModel.DataAnnotations;

namespace GrassHopper.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required(ErrorMessage = "Reviewer name is required.")]
        [StringLength(50, ErrorMessage = "Reviewer name can't be longer than 50 characters.")]
        public string ReviewerName { get; set; }

        [Required(ErrorMessage = "Review body is required.")]
        [StringLength(528, ErrorMessage = "Review body can't be longer than 528 characters.")]
        public string ReviewBody { get; set; }

        [Required(ErrorMessage = "Review rating is required.")]
        [Range(1, 5, ErrorMessage = "Must Select Between 1-5 stars")]
        public int ReviewRating { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }
    }
}