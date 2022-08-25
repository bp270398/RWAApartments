using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Public.Models.View_Model
{
    public class ApartmentReviewVM
    {
        [Range(1, 5, ErrorMessage = "Please enter the correct value")]
        public int Stars { get; set; }
        public int MaxStars { get; set; } = 5;
        public string Comment { get; set; }
    }
}