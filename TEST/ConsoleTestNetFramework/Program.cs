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

            object fileName = @"C:\Users\aleks\Downloads\zakaz-naryad2.doc";
            object falseValue = false;
            object trueValue = true;
            object missing = Type.Missing;

            doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing);

            //Указываем таблицу в которую будем помещать данные (таблица должна существовать в шаблоне документа!)
            Microsoft.Office.Interop.Word.Table tbl = app.ActiveDocument.Tables[3];

            //Заполняем в таблицу - 10 записей.

            for (int i = 1; i < 11; i++)
            {
                tbl.Rows.Add(ref missing);//Добавляем в таблицу строку.
                                          //Обычно саздаю только строку с заголовками и одну пустую для данных.
                tbl.Rows[i + 1].Cells[1].Range.Text = Convert.ToString(i);
                tbl.Rows[i + 1].Cells[2].Range.Text = "Запись №" + i.ToString();
                tbl.Rows[i + 1].Cells[3].Range.Text = "Запись №" + i.ToString();

            }

            //Открываем документ для просмотра.
            app.Visible = true;
        }
    }
}
