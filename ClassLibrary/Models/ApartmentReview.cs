using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    [Serializable]
    public class ApartmentReview
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Apartment Apartment { get; set; }
        public User User { get; set; }


        public String Details { get; set; }
        public int Stars { get; set; }
    }
}
