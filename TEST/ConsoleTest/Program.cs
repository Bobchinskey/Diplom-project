using System;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Время указывается в миллисекундах
            Timer timer = new Timer(showTime, null, 0, 2000);
            // Таймер будет работать до тех пор, пока мы не нажмём Enter
            Console.ReadLine();
        }
        static void showTime(Object obj)
        {
            Console.WriteLine("Текущие дата и время: {0}", DateTime.Now.ToString());
        }
    }
}
