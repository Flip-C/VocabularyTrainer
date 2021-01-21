using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Biermann_Erlacher_VokabelTrainer
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
            string[] languages = lines[0].Split(';');
            string[] words = new string[languages.Length];

           
            foreach (var line in lines)
            {
                    words = line.Split(';');
                    translationList.Add(new Translator {Languages = languages, Translations = words });
            }


            //erste line sind die sprachen, rausfiltern und zu jedem object dazu geben
            //jede weiter line in ein array      


            //each row = new Translator with 2 arrays as properties
            //languageArray and translationArray
            //translationList.Add(lang)
        }


        //neue Übersetzung hinzufügen 
        //Eingabe im Main
        //übergabe von neuen wörtern
        public void AddNewWords()
        {

        }


        //übersetzung eines wortes und Ursprungswort wird übergeben
        //Methode check ob übersetzung stimmt
        //return true/false
        public void CheckingTranslation()
        {
             
        }


        //bekommt sprache übergeben von welche sie eine Wort zufällig ausgeben soll
        //gibt ein zufälliges wort in der Liste der jeweils ausgewählten sprache wieder
        //return string
        public string RandomWord()
        {
            return null;
        }
        #endregion
    }
}
