using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Rate
{
    class ListRateInformation
    {
        public int num { get; set; }
        public int id_rate { get; set; }
        public int id_vehicle { get; set; }
        public string make_model { get; set; }
        public string Rate1_3 { get; set; }
        public string Rate4_9 { get; set; }
        public string Rate10_29 { get; set; }
        public string Rate30 { get; set; }
        public string Deposit { get; set; }
        public string excess_mileage { get; set; }
        public string reality { get; set; }
    }
}
