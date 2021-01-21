using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Biermann_Erlacher_Vokabeltrainer
{
    class VocabularyManager
    {
        #region members
        List<Translator> translationList;
        #endregion

        #region constructor
        public VocabularyManager()
        {
            List<Translator> translationList = new List<Translator>();
        }
        
        
        #endregion

        #region properties
        #endregion

        #region methods
        public void CsvParser(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            string[] language = lines[0].Split(';');
            string[] words = new string[language.Length];


            foreach (var line in lines)
            {
                    words = line.Split(';');
                    Translator translation = new Translator(language, words);
                    translationList.Add(translation);
            }
            //erste line sind die sprachen, rausfiltern und zu jedem object dazu geben
            //jede weiter line in ein array      


            //each row = new Translator with 2 arrays as properties
            //languageArray and translationArray
            //translationList.Add(lang)
        }

        public void AddNewTranslation()
        {

        }

        public void CheckingTranslation()
        {

        }
        #endregion
    }
}
