using System;

namespace Partner.Models.InformativeWindowModels
{
    class ImportantInformationModel
    {
        public int id_important_information { get; set; }
        public string heading { get; set; }
        public DateTime date_publication { get; set; }
        public string important_information { get; set; }
    }
}
