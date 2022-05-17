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
            
            //Создаём новый Word.Application
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

            //Загружаем документ
            Microsoft.Office.Interop.Word.Document doc = null;

            object fileName = @"C:\Users\aleks\OneDrive\Рабочий стол\Diplom project\22.docx";
            object falseValue = false;
            object trueValue = true;
            object missing = Type.Missing;

            doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing);

            //Теперь у нас есть документ который мы будем менять.

            //Очищаем параметры поиска
            app.Selection.Find.ClearFormatting();
            app.Selection.Find.Replacement.ClearFormatting();

            //Задаём параметры замены и выполняем замену.
            object findText = "<DD>";
            object replaceWith = "№88";
            object replace = 2;

            app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
            ref replace, ref missing, ref missing, ref missing, ref missing);
            
            //Задаём параметры замены и выполняем замену.
            /*findText = "<kk>";
            replaceWith = "Иванов А.Т.";
            replace = 2;

            app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
            ref replace, ref missing, ref missing, ref missing, ref missing);
            */
            //Открываем документ для просмотра.
            app.Visible = true;
            /*
            // Создаём объект документа
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = null;
            try
            {
                

                //Загружаем документ
                

                object fileName = @"C:\Users\aleks\OneDrive\Рабочий стол\Diplom project\22.docx";
                object falseValue = false;
                object trueValue = true;
                object missing = Type.Missing;

                doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing);
                // Открываем
               

                // Добавляем информацию
                // wBookmarks содержит все закладки
                Word.Bookmarks wBookmarks = doc.Bookmarks;
                Word.Range wRange;
                int i = 0;
                string[] data = new string[3] { "27", "Договор", "18"};
                foreach (Word.Bookmark mark in wBookmarks)
                {

                    wRange = mark.Range;
                    wRange.Text = data[i];
                    i++;
                }
                app.Visible = true;
            }
            catch (Exception ex)
            {
                // Если произошла ошибка, то
                // закрываем документ и выводим информацию
                doc.Close();
                doc = null;
                Console.WriteLine("Во время выполнения произошла ошибка!");
                Console.ReadLine();
            }
            */
        }
    }
}
