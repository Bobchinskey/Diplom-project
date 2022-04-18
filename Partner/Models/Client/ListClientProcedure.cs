using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Client
{
    class ListClientProcedure
    {
        public static int id_natural_person { get; set; }
        public static string surname { get; set; }
        public static string name { get; set; }
        public static string patronymic { get; set; }
        public static DateTime birthday { get; set; }
        public static string place_birthday { get; set; }
        public static string INN { get; set; }
        public static string series_passport { get; set; }
        public static string number_passport { get; set; }
        public static string who_issued_passport { get; set; }
        public static DateTime date_issued_passport { get; set; }
        public static string number_card { get; set; }
        public static DateTime validity_period_card { get; set; }
        public static string CVC2 { get; set; }
        public static string registration { get; set; }
        public static string actual_place_residence { get; set; }
        public static string phone_number { get; set; }
        public static string email { get; set; }
        public static object image { get; set; }
        public static string reality { get; set; }
        public static string who_add_system { get; set; }
        public static string date_add_system { get; set; }
        public static string EditOrAdd { get; set; }
    }
}
