using System;
using Microsoft.Office.Interop.Word;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Создаём новый Word.Application
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

            //Загружаем документ
            Microsoft.Office.Interop.Word.Document doc = null;

            object fileName = @"C:\\Users\aleks\Downloads\ДОГОВОР аренды  654-.doc";
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
            object findText = "<Что меняем>";
            object replaceWith = "<На что меняем>";
            object replace = 2;

            app.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceWith,
            ref replace, ref missing, ref missing, ref missing, ref missing);



            //Открываем документ для просмотра.
            app.Visible = true;
        }
    }
}
