using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Public.Models
{
    public class ContactForm
    {
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        [Required(ErrorMessage = "Please enter your first name.")]
        [MaxLength(90), MinLength(1)]
        public string FName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Please enter your Last name.")]
        [MaxLength(90), MinLength(1)]
        public string LName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter your email.")]
        [MaxLength(90), MinLength(1)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Please enter your phone number.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Adults")]
        [Required(ErrorMessage = "Please enter the number of adults.")]
        public int? Adults { get; set; }

        [Display(Name = "Children")]
        [Required(ErrorMessage = "Please enter the number of children.")]
        public int? Children { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Arrival date")]
        [Required(ErrorMessage = "Please enter the arrival date.")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Arrival { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Departure date")]
        [Required(ErrorMessage = "Please enter the departure date.")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Departure { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Comment")]
        public string Comment  { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public int ApartmentId { get; set; }

        public string GetDetails()
        {
            string details = "";
            if (Arrival != null && Departure != null)
            {
                details += Arrival.ToShortDateString() + " - " + Departure.ToShortDateString();
            }
            if (Adults.HasValue && Children.HasValue)
            {
                details += $"; {Adults.Value} adults + {Children.Value} children";
            }
            if(Comment != null && !Comment.Equals(""))
            {
                details += $"; Comment : {Comment}";
            }
            return details;
        }

    }
}