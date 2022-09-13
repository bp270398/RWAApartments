using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Created at")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.  hh:mm}",  ApplyFormatInEditMode = true)]
        public string CreatedAt { get; set; }
        public string DeletedAt { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter a valid email.")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }
        public string SecurityStamp { get; set; }

        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please enter your phone number.")]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        [Display(Name = "Address")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter your address.")]
        public string Address { get; set; }

        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter your username.")]
        [UsernameExists(ErrorMessage = "Username already exists.")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your password.")]
        public string PasswordHash { get; set; }

    }
}
