using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Vehicle
{
    class ListVehicle
    {
        public int num { get; set; }
        public int id_vehicle { get; set; }
        public string MakeModel { get; set; }
        public string VIN { get; set; }
        public string StateNumber { get; set; }
        public string SelectStateNumber { get; set; }
        public string YearManufacture { get; set; }
        public string SelectCategory { get; set; }
        public string NumberEngine { get; set; }
        public string ChassisNumber { get; set; }
        public string BodyNumber { get; set; }
        public string Color { get; set; }
        public string EnvironmentalClass { get; set; }
        public string PowerEngine { get; set; }
        public string DisplacementEngine { get; set; }
        public string TypeEngine { get; set; }
        public string PermittedMaximumMass { get; set; }
        public string WeightWithoutLoad { get; set; }
        public string SeriesPTS { get; set; }
        public string NumberPTS { get; set; }
        public string WhoIssuedPTS { get; set; }
        public DateTime DateIssuedPTS { get; set; }
        public string SeriesSTS { get; set; }
        public string NumberSTS { get; set; }
        public string WhoIssuedSTS { get; set; }
        public DateTime DateIssuedSTS { get; set; }
        public object Image { get; set; }
        public string Status { get; set; }
        public int WhAddSystem { get; set; }
        public DateTime DateAddSystem { get; set; }
        public string EditOrAdd { get; set; }
    }
}
