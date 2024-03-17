using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GH.Models
{
    public class ReviewModel
    {

        [Key]
        public int ReviewId { get; set; }
        public string Review { get; set; }

        public DateTime Date { get; set; }
        [Range(1, 5)]
        public int? Rating { get; set; }
    }
}
