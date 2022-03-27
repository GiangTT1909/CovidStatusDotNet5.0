using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class CountryLocation
    {
        public string country { get; set; }
        public string alpha2 { get; set; }
        public string alpha3 { get; set; }
        public int numberic { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
