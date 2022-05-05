using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Insurances
{
    class EditOrAddInsurancesModel
    {
        public static bool ActionCombobox { get; set; }
        public static string Title { get; set;}
        public static int ID { get; set; }
        public static int id_vehicle { get; set; }
        public static string make_model { get; set; }
        public static string Type { get; set; }
        public static string Series { get; set; }
        public static string Number { get; set; }
        public static DateTime start_date { get; set; }
        public static DateTime end_dete { get; set; }
    }
}
