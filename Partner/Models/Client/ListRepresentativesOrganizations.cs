using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Client
{
    class ListRepresentativesOrganizations
    {
        public static int id_representatives_organizations { get; set; }
        public static int id_legal_entity { get; set; }
        public static bool genderMan { get; set; }
        public static bool genderWoman { get; set; }
        public static string editoradd { get; set; }
        public static string FIO { get; set; }
        public static string Surname { get; set; }
        public static string Name { get; set; }
        public static string Patronymic { get; set; }
        public static string Post { get; set; }
        public static string Gender { get; set; }
        public static string SeriesPassport { get; set; }
        public static string NumberPassport { get; set; }
        public static string PhoneNumber { get; set; }
        public static int who_add_system { get; set; }
        public static DateTime date_add_system { get; set; }
    }
}
