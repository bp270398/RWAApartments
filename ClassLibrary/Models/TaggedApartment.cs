using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    [Serializable]
    public class TaggedApartment
    {
        public int? Id { get; set; }
        public int ApartmentId { get; set; }
        public int TagId { get; set; }
    }
}
