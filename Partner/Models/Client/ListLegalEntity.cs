using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Client
{
    class ListLegalEntity
    {
        public static int id_legal_entity { get; set; }
        public static string name_organization { get; set; }
        public static string abbreviated_name_organization { get; set; }
        public static string surname_director { get; set; }
        public static string name_director { get; set; }
        public static string patronymic_director { get; set; }
        public static string legal_address { get; set; }
        public static string actual_legal_address { get; set; }
        public static string INN { get; set; }
        public static string KPP { get; set; }
        public static string OGRN { get; set; }
        public static string payment_account { get; set; }
        public static string BIK { get; set; }
        public static string email { get; set; }
        public static string fax { get; set; }
        public static string phone_number { get; set; }
        public static string website { get; set; }
    }
}
