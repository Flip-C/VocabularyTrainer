using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Biermann_Erlacher_VokabelTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "..//..//..//TranslationFiles//Übersetzungen.csv";
            bool success = File.Exists(filePath);
            
            VocabularyManager manager = new VocabularyManager();
            manager.CsvParser(filePath);

        }

        static void AddTranslation()
        {
           
        }

        static void VocabularyTest()
        {

        }
    }
}
