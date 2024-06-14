using System.ComponentModel.DataAnnotations;

namespace GrassHopper.Models
{
    public class Quote
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Invalid phone number format. Must be a valid phone number EX:123-234-5678")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Body is required.")]
        [StringLength(500, ErrorMessage = "Body can't be longer than 528 characters.")]
        public string Body { get; set; }
    }
}