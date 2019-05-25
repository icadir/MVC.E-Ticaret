
using System.Collections.Generic;

namespace ETicaret.Models
{
    public class ProfilModels
    {
        public DB.Members Members { get; set; }
        public List<DB.Address> Addresseses { get; set; }
        public DB.Address CurrentAddress { get; set; } 
    }
}