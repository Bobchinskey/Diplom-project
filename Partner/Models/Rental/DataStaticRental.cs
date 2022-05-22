using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Rental
{
    class DataStaticRental
    {
        public static int IDContract { get; set; }
        public static int IDClient { get; set; }
        public static int IDRepresentativesOrganization { get; set; }
        public static string FIORepresentativesOrganization { get; set; }
        public static string PostRepresentativesOrganization { get; set; }
        public static string Title { get; set; }
        public static DateTime StartDateRental { get; set; }
        public static DateTime EndDateRental { get; set; }
        public static string Type { get; set; }
        public static string Edit { get; set; }
        public static string TypeActions { get; set; } 
    }
}
