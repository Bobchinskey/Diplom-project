using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Insurances
{
    class ListMainInsuranceData
    {
        public int num { get; set; }
        public int id_osago { get; set; }
        public int id_vehicle { get; set; }
        public string make_model { get; set; }
        public string type { get; set; }
        public string series { get; set; }
        public string number { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string reality { get; set; }
    }
}
