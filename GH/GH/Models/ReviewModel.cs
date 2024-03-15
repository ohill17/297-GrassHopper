using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GH.Models
{
    public class ReviewModel
    {
        [Key]
        public int ReviewId { get; set; }

        [StringLength(500, MinimumLength = 5)]
        public string Review { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; }
    }
}
