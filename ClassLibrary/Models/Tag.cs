using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    [Serializable]
    public class Tag
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public TagType Type { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public int Usage { get; set; }
    }
}
