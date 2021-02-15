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
        //parse csv words into a list - each element of the list is a translation
        public void CsvParser(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string readedLanguages = reader.ReadLine();
                    languagesArray = readedLanguages.Split(';');
                    string[] translations = new string[languagesArray.Length];

                    while (reader.Peek() > 0)
                    {
                        string line = reader.ReadLine();
                        if (!line.StartsWith(languagesArray[0]))
                        {
                            translations = line.Split(';');
                            this.translationList.Add(new Translator { Languages = languagesArray, Translations = translations });
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //adds words to csv File
        public void AddWordsToCSV(string filePath, string[] newTranslation)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    string output = "";
                    foreach (string word in newTranslation)
                    {
                        output += word + ";";
                    }
                    output = output.Remove(output.Length - 1);
                    writer.Write(output);
                }
            }
            catch (Exception)
            {

                throw;
            }         
        }

        //adds new words to the list
        public void AddNewWordsToList(Translator newTranslator)
        {
            try
            {
                translationList.Add(newTranslator);
            }
            catch (Exception)
            {
                throw;
            }        
        }

        //checking if input word is right translation
        public bool CheckingTranslation(string comparingWord, string inputWord, int firstLanguageIndex, int secLanguageIndex)
        {
            string correctTranslation;
            try
            {
                correctTranslation = translationList.Find(x => x.Translations[firstLanguageIndex].Contains(comparingWord)).GetTranslations(secLanguageIndex);
            }
            catch (Exception)
            {
                throw;
            }
            
            if (correctTranslation == inputWord)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Find Random word in the language the user had choosen
        //if there is no word, find a new random index
        public string RandomWord(int firstLanguageIndex,int secLanguageIndex)
        {
            bool wordInside = true;
            string randomWord;
            string randomWordTranslation;
            do
            {
                var random = new Random();
                int index = random.Next(translationList.Count);
                Translator randomTranslation = translationList[index];
                randomWord = randomTranslation.Translations[firstLanguageIndex];
                randomWordTranslation = randomTranslation.Translations[secLanguageIndex];

                if (randomWord == "" || randomWordTranslation == "")
                {
                    wordInside = false;
                }
                else
                {
                    wordInside = true;
                }
            } while (!wordInside);

            return randomWord;
        }

        //returns the number of languages
        public int NumberOfTranslations()
        {
            return translationList.Count;
        }
        #endregion


        //returns actual avaible languages
        #region static methods
        public string[] GetLanguages()
        {
            return languagesArray;
        }
        #endregion

    }
}
