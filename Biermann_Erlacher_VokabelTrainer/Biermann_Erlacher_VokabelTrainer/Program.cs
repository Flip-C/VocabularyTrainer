using System;

namespace Biermann_Erlacher_VokabelTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Erlacher Fabian\Desktop\Programmieren\Vokabel Trainer\Biermann_Erlacher_VokabelTrainer\Biermann_Erlacher_VokabelTrainer\Übersetzungen.csv";

            VocabularyManager manager = new VocabularyManager();
            manager.CsvParser(filePath);
            Console.WriteLine("Test");

            Console.WriteLine("CommitTest2");
        }

        static void AddTranslation()
        {
           
        }

        static void VocabularyTest()
        {

        }
    }
}
