using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Models.Client
{
    class ListNaturalPersonAdditionalData
    {
        public int num { get; set; }
        public string phone_number { get; set; }
        public string other { get; set; }
    }

    class EditorAdd
    {
        public static string phone_number { get; set; }
        public static string other { get; set; }
        public static string editoradd { get; set; }
    }
}
