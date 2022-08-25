using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    [Serializable]
    public class ApartmentPicture
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Apartment Apartment { get; set; }
        public String Path { get; set; }
        public String Name { get; set; }
        public bool? IsRepresentative { get; set; }
    }
}
