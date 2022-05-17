using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Client
{
    class ListRepresentativesOrganizationsData
    {
        public int id_representatives_organizations { get; set; }
        public int num { get; set; }
        public string FIO { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Post { get; set; }
        public string Gender { get; set; }
        public string SeriesPassport { get; set; }
        public string NumberPassport { get; set; }
        public string PhoneNumber { get; set; }
    }
}
