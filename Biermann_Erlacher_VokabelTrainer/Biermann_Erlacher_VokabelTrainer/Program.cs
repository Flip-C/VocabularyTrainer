using System;

namespace Biermann_Erlacher_Vokabeltrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Erlacher Fabian\Desktop\Programmieren\VokabelTrainer\Erlacher_Biermann_Vokabeltrainer\Erlacher_Biermann_Vokabeltrainer\Übersetzungen.csv";

            VocabularyManager manager = new VocabularyManager();
            manager.CsvParser(filePath);
            Console.WriteLine("Test");
        }

        static void AddTranslation()
        {

        }

        static void VocabularyTest()
        {

        }
    }
}
