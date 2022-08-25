using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public.Models.View_Model
{
    public class ApartmentVM
    {
        public Apartment Apartment { get; set; }
        public IEnumerable<ApartmentPicture> Pictures { get; set; }
        public IEnumerable<ApartmentReview> Reviews { get; set; }
        public double Rating { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public ContactForm ContactForm { get; set; }
    }
}