using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public.Models.View_Model
{
    public class UserVM
    {
        public User User { get; set; }
        public IList<ApartmentReview> Reviews { get; set; }
        public IList<ApartmentReservation> Reservations { get; set; }

    }
}