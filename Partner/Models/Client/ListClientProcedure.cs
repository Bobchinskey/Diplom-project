using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Client
{
    class ListClientProcedure
    {
        public int id_natural_person { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string INN { get; set; }
        public string series_passport { get; set; }
        public string number_passport { get; set; }
        public string who_issued_passport { get; set; }
        public DateTime date_issued_passport { get; set; }
        public string number_card { get; set; }
        public DateTime validity_period_card { get; set; }
        public string CVC2 { get; set; }
        public string registration { get; set; }
        public string actual_place_residence { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public object image { get; set; }
        public string reality { get; set; }
        public string who_add_system { get; set; }
        public string date_add_system { get; set; }
    }
}
