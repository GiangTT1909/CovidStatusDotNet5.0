using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CountryPieDataDTO
    {
        public string ID { get; set; }
        public string country { get; set; }
        public string CountryCode { get; set; }
        public int TotalConfirmed { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalRecovered { get; set; }
        public DateTime Date { get; set; }
        public String Slug { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
