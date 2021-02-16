﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Biermann_Erlacher_VokabelTrainer
{
    class Program
    {
        static string filePath = "..//..//..//TranslationFiles//Übersetzungen.csv";
        static void Main(string[] args)
        {
            bool exception = true;
            bool readFileAgain = true;
            string input;
            VocabularyManager vocabularyList = new VocabularyManager();

            //Try to read file - if exception -> try again or quit
            do
            {
                try
                {
                    vocabularyList.CsvParser(filePath);
                    exception = false;
                    readFileAgain = false;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Die Datei mit dem Namen -Übersetzungen.csv- befindet sich nicht im angegebenen Ordner.");
                    exception = true;
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Der Angegebene Datei Pfad exestiert nicht.");
                    exception = true;
                }
                catch (IOException)
                {
                    Console.WriteLine("Die Datei wird gerade wo anders benutzt. Bitte schließen sie die Datei und versuchen sie es nochmal");
                    exception = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Ein Unbekannter Fehler ist aufgetreten.");
                    exception = true;
                }
                if (exception)
                {
                    Console.WriteLine("A - drücken um zu wiederholen");
                    Console.WriteLine("Beliebige Taste drücken um zu beenden");
                    input = Console.ReadLine();

                    if (input == "A" || input == "a")
                    {
                        readFileAgain = true;
                    }
                    else
                    {
                        return;
                    }
                    
                }
            } while (readFileAgain);

            Console.WriteLine("Die Datei wurde soeben erfolgreich eingelesen");
            Console.WriteLine("Willkommen zum Vokabeltrainer");
            
            do
            {
                Console.WriteLine("Um der Liste ein neues Wort anzuhängen drücken sie bitte >A<");
                Console.WriteLine("Um auf 10 Vokabeln geprüft zu werden drücken Sie bitte >T<");
                Console.WriteLine("Um das Programm zu beenden drücken Sie bitte >E<");
                string inputChoice = Console.ReadLine();
                               

                switch (inputChoice)
                {
                    case "a": AddTranslation(vocabularyList); break;
                    case "A": AddTranslation(vocabularyList); break;
                    case "t": VocabularyTest(vocabularyList); break;
                    case "T": VocabularyTest(vocabularyList); break;
                    case "e": return;
                    case "E": return;
                    default:                     
                        Console.WriteLine("Falsche Eingabe, bitte Buchstaben eingeben");
                        break;
                }
            } while (true);            
        }

        static void AddTranslation(VocabularyManager vocabularyList)
        {
            bool inputCorrect = true;
            bool switchSuccess = true;
            string[] languageArray = vocabularyList.GetLanguages();
            string[] newTranslations = new string[languageArray.Length];
            int numberOfNewWords;

            //asking for number of new words
            Console.WriteLine("Wie viele neue Wörter wollen sie hinzufügen");
            do
            {
                inputCorrect = true;
                if (!int.TryParse(Console.ReadLine(), out numberOfNewWords))
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                    inputCorrect = false;
                }
            } while (!inputCorrect);

            //Big for-loop for adding new words
            for (int i = 0; i < numberOfNewWords; i++)
            {
                do
                {
                    //input the translations of the word
                    for (int j = 0; j < languageArray.Length; j++)
                    {
                        Console.WriteLine("Wort in {0} eingeben", languageArray[j]);
                        newTranslations[j] = Console.ReadLine();
                    }

                    //user checks input
                    Console.WriteLine("\nKontrollieren sie ihre Eingabe.");
                    Console.WriteLine("W - Eingabe Widerholen");
                    Console.WriteLine("E - Eingabe Bestätigen");
                    Console.WriteLine("Ihre erste eingabe lautet:\n");

                    //Output languages and input words
                    foreach (var language in languageArray)
                    {
                        Console.Write(language.PadRight(17, ' '));
                    }
                    Console.WriteLine();
                    foreach (var word in newTranslations)
                    {
                        Console.Write(word.PadRight(17, ' '));
                    }
                    Console.WriteLine();
                    do
                    {
                        //user decide if input is correct
                        string choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "W": inputCorrect = false; switchSuccess = true; break;
                            case "w": inputCorrect = false; switchSuccess = true; break;
                            case "E": inputCorrect = true; switchSuccess = true; break;
                            case "e": inputCorrect = true; switchSuccess = true; break;
                            default:
                                Console.WriteLine("Ungültige Eingabe. Bitte Widerholen");
                                switchSuccess = false;
                                break;
                        }
                    } while (!switchSuccess);

                } while (!inputCorrect);

                //creating new translation and add new Translation to the list
                Translator translation = new Translator(newTranslations, languageArray);
                try
                {
                    vocabularyList.AddNewWordsToList(translation);
                    vocabularyList.AddWordsToCSV(filePath, newTranslations);
                }
                catch (Exception )
                {
                    Console.WriteLine("Der Dienst ist momentan nicht verfügbar");
                }               
            }

            Console.WriteLine("\nWollen sie noch weitere Wörter hinzufügen schreiben sie ja");
            Console.WriteLine("Ansonsten beliebige Taste und enter drücken");
            if (Console.ReadLine() == "ja" || Console.ReadLine() == "Ja")
            {
                AddTranslation(vocabularyList);
            }
        }


                                
        static void VocabularyTest(VocabularyManager vocabularyList)
        {
            Console.WriteLine("Welche Sprache soll gelernt werden?");
            string[] translationArray = vocabularyList.GetLanguages();
            for (int i = 0; i < translationArray.Length; i++)
            {                
                Console.Write("{0} {1} ", i+1, translationArray[i]);               
            }            

            //Eingabe vom User kontrollieren also die EIngabe darf nicht höher sein als Spalten/Sprachen im LanguageArray sein 
            //ifbedingung mit else {index existiert nicht} eingabe wiederholen -> succes für die If Schleife (false) solange es false ist wiederholen 
                        
            bool lang1Correct;
            int userLang1;

            Console.WriteLine("\nErste Sprache");
            do
            {
                lang1Correct = true;               
                
                if (!int.TryParse(Console.ReadLine(), out userLang1))
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                    lang1Correct = false;
                }                
                if (userLang1 >= translationArray.Length)
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                    lang1Correct = false;
                }
            } while (!lang1Correct);

            bool lang2Correct;
            int userLang2;

            Console.WriteLine("\nZweite Sprache");
            do
            {
                lang2Correct = true;               

                if (!int.TryParse(Console.ReadLine(), out userLang2)|| userLang2 >= translationArray.Length)
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                    lang2Correct = false;
                }                
            } while (!lang2Correct);            

            int languageIndex1=userLang1-1;    
            int languageIndex2=userLang2-1;


            
            int choice;
            bool choiceSucces;
            Console.WriteLine("In welche Sprache möchten Sie übersetzen?\n1.{0}->{1} oder 2.{1}->{0}", translationArray[languageIndex1], translationArray[languageIndex2]);
            do
            {
                choiceSucces = true;
               
                if(!int.TryParse(Console.ReadLine(),out choice))
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                    choiceSucces = false;
                }       
                if(choice > 2 || choice < 1)
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                    choiceSucces = false;
                }
            } while (!choiceSucces);                                         
            
            if (choice == 1)
            {
                languageIndex1 = userLang1-1;
                languageIndex2 = userLang2-1;                
            }

            if (choice == 2)
            {
                languageIndex1 = userLang2-1;
                languageIndex2 = userLang1-1;
            }

            int counterRightWords = 0;

            for (int i = 0; i < 10; i++)
            {
                string comparingWord = vocabularyList.RandomWord(languageIndex1,languageIndex2);
                Console.WriteLine();
                Console.WriteLine(comparingWord);                
                Console.WriteLine("Übersetzen Sie das Wort in {0}", translationArray[languageIndex2]);

                string inputWord = Console.ReadLine(); //Lösung vom User

                bool success1 = vocabularyList.CheckingTranslation(comparingWord, inputWord, languageIndex1, languageIndex2);
                if (success1)
                {
                    Console.WriteLine("Wort ist richtig");
                    counterRightWords++;
                }
                else
                {
                    Console.WriteLine("Wort ist falsch");
                }

            }
            Console.WriteLine("Sie haben {0} von 10 Wörter richtig\n", counterRightWords);
        }
    }
}

