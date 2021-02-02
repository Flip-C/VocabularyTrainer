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

        #region static members
        static string[] languagesArray;
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
            languagesArray = readedLines[0].Split(';');
            string[] translations = new string[languagesArray.Length];

            foreach (string line in readedLines)
            {
                if (!line.StartsWith(languagesArray[0]))
                {
                    translations = line.Split(';');
                    this.translationList.Add(new Translator { Languages = languagesArray, Translations = translations });
                }
            }

            /*
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (reader.Peek() > 0)
                {
                    string line = reader.ReadLine();
                    if
                    string[] input = line.Split(';');
                    languagesArray = input[0].Split(';');

                    this.translationList.Add(new Translator { Languages = languagesArray, Translations = translations });
                }
            }
            */
            
        }


        //neue Übersetzung hinzufügen 
        //Eingabe im Main von neuen Wörtern
        //übergabe von neuen wörtern
        //neue wörter zur liste hinzufügen
        //Im Main überprüfen und User auf etwaiige Fehler hinweisen
        //return true or false
        public bool AddNewWordsToList(Translator newTranslator)
        {
            try
            {
                translationList.Add(newTranslator);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }        
        }


        //Reihenfolge der Übergabeparameter, random Wort, userinputword, indexRandomWord, indexUserinputWord
        //Methode check ob übersetzung stimmt
        //return true/false
        public bool CheckingTranslation(string comparingWord, string inputWord, int firstLanguageIndex, int secLanguageIndex)
        {           
           string correctTranslation = translationList.Find(x => x.Translations[firstLanguageIndex].Contains(comparingWord)).GetTranslations(secLanguageIndex);
            if (correctTranslation == inputWord)
            {
                return true;
            }
            else
            {
                return false;
            }    
        }


        //bekommt sprache übergeben von welche sie eine Wort zufällig ausgeben soll
        //gibt ein zufälliges wort in der Liste der jeweils ausgewählten sprache wieder
        //return zufälliges Wort einer bestimmten sprache
        public string RandomWord(int firstLanguageindex)
        {            
            var random = new Random();
            //Find Random Index of the List
            int index = random.Next(translationList.Count); 
            //Find A Random Translation in the List
            Translator randomTranslation = translationList[index]; 
            //Wählt anhand der vom User ausgewählten sprache die richtige übersetzung
            string word = randomTranslation.Translations[firstLanguageindex]; 
            
            return word;
        }

        //Gibt zurück wie viele verschieden Wörter es gibt
        public int NumberOfTranslations()
        {
            return translationList.Count;
        }
        #endregion


        //Gibt ein array zurück wo die aktuellen Sprachen gespeichert sind. z.B languagesArray[0] = deutsch; ..[1] = englisch usw...
        #region static methods
        public string[] GetLanguages()
        {
            return languagesArray;
        }
        #endregion

    }
}
