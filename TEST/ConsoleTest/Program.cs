using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string phrase = "The quick brown fox,umps over,the lazy dog.";
            string[] words = phrase.Split(',');

            foreach (var word in words)
            {
                System.Console.WriteLine($"<{word}>");
            }
        }
    }
}
