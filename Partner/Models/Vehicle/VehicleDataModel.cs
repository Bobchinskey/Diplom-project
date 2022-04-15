using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Vehicle
{
    class VehicleDataModel
    {
        public static int id_vehicle { get; set; }
        public static string MakeModel { get; set; }
        public static string VIN { get; set; }
        public static string StateNumber { get; set; }
        public static string SelectStateNumber { get; set; }
        public static string YearManufacture { get; set; }
        public static string SelectCategory { get; set; }
        public static string NumberEngine { get; set; }
        public static string ChassisNumber { get; set; }
        public static string BodyNumber { get; set; }
        public static string Color { get; set; }
        public static string EnvironmentalClass { get; set; }
        public static string PowerEngine { get; set; }
        public static string DisplacementEngine { get; set; }
        public static string TypeEngine { get; set; }
        public static string PermittedMaximumMass { get; set; }
        public static string WeightWithoutLoad { get; set; }
        public static string SeriesPTS { get; set; }
        public static string NumberPTS { get; set; }
        public static string WhoIssuedPTS { get; set; }
        public static DateTime DateIssuedPTS { get; set; }
        public static string SeriesSTS { get; set; }
        public static string NumberSTS { get; set; }
        public static string WhoIssuedSTS { get; set; }
        public static DateTime DateIssuedSTS { get; set; }
        public static object Image { get; set; }
        public static string Status { get; set; }
        public static int WhAddSystem { get; set; }
        public static DateTime DateAddSystem { get; set; }
        public static string EditOrAdd { get; set; }
    }
}
