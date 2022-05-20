using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace ConsoleTestNetFramework
{
    class Program
    {
        static void Main(string[] args)
        {

            string s1 = " ИП 2 ВОЛГОООПЕРАТОР";
            string ch = "ИП ";
            int indexOfChar = s1.IndexOf(ch); // равно 4
            Console.WriteLine(indexOfChar);
            Console.ReadKey();
        }
    }
}
