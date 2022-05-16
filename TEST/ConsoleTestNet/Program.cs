using System;
using Microsoft.Office.Interop.Word;

namespace ConsoleTestNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();
            Document doc = app.Documents.Open(@"C:\\Users\aleks\Downloads\22.doc");
            string bookmark = "Договор";

            Bookmark bm = doc.Bookmarks[bookmark];
            Range range = bm.Range;
            range.Text = "Hello World";
            doc.Bookmarks.Add(bookmark, range);
        }
    }
}
