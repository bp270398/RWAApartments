using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    [Serializable]
    public class ApartmentReservation
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Apartment Apartment { get; set; }
        public string Details { get; set; }
        public User User { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
