using System;

namespace Partner.Models.InformativeWindowModels
{
    class NewsModel
    {
        public int id_news { get; set; }
        public string heading { get; set; }
        public DateTime date_publication { get; set; }
        public string news { get; set; }
    } 
}
