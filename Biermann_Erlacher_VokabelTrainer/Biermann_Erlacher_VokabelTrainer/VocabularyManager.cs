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
            this.translationList = new List<Translator>();
        }
        
        
        #endregion

        #region properties
        #endregion

        #region methods
        public void CsvParser(string filePath)
        {
            string[] readedLines = File.ReadAllLines(filePath);
            string[] languages = readedLines[0].Split(';');
            string[] translations = new string[languages.Length];


            foreach (string line in readedLines)
            {
                if (!line.StartsWith(languages[0]))
                {
                    translations = line.Split(';');
                    this.translationList.Add(new Translator { Languages = languages, Translations = translations });
                }
            }

            //erste line sind die sprachen, rausfiltern und zu jedem object dazu geben
            //jede weiter line in ein array = Übersetzungen     

            //each row = new Translator with 2 arrays as properties
            //languageArray and translationArray
            //translationList.Add(lang)
        }


        //neue Übersetzung hinzufügen 
        //Eingabe im Main
        //übergabe von neuen wörtern
        //neue wörter zur liste hinzufügen
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
