using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model { 
    public class DataSumaryModel
    {
        public string ID { get; set; }
        public string Message { get; set; }
        public Global Global { get; set; }
        public List<Country> Countries { get; set; }
        public DateTime Date { get; set; }

    }
}
