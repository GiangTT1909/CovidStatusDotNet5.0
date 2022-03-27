using System;
using System.Collections.Generic;
using System.Text;
namespace DataAccess.Model
{
    public class Country
    {
        public string ID { get; set; }
        public string country { get; set; }
        public string CountryCode { get; set; }
        public string Slug { get; set; }
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }
        public DateTime Date { get; set; }
        public Premium Premium { get; set; }

       
        public override string ToString() {
            return this.country;
        }
    }
}
