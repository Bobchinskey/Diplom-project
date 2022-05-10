using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Maintenances
{
    class ListDataMaintenance
    {
        public int num { get; set; }
        public int id_maintenance { get; set; }
        public string name_maintenance { get; set; }
        public DateTime start_date_maintenance { get; set; }
        public DateTime end_date_maintenance { get; set; }
    }
}
