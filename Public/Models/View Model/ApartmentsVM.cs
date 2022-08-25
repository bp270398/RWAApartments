using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Public.Models.View_Model
{
    public class ApartmentsVM
    {

        public Dictionary<int, ApartmentVM> ApartmentVMs { get; set; }
       
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<City> Cities { get; set; }

        public int? CityId { get; set; }

        [Display(Name ="Only available properties")]
        public bool IsAvailable { get; set; }
        public int? ApartmentId { get; set; }

        public int? Rooms { get; set; }
        public int? Adults { get; set; }
        public int? Children { get; set; }

        
    }
}