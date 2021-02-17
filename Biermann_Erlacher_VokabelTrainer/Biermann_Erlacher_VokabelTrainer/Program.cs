using System;
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

            //Information file is successfully loaded
            Console.WriteLine("Die Datei wurde soeben erfolgreich eingelesen.");
            Console.WriteLine("Willkommen zum Vokabeltrainer.");

            //choose function 
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

        //methode for adding new words to the file
        static void AddTranslation(VocabularyManager vocabularyList)
        {
            bool enteredCorrectly = true;
            bool validInput = true;
            string[] languageArray = vocabularyList.GetLanguages();
            string[] newTranslations = new string[languageArray.Length];
            int numberOfNewWords;

            //asking for number of new words
            Console.WriteLine("Wie viele neue Wörter wollen sie hinzufügen");
            do
            {
                enteredCorrectly = true;
                if (!int.TryParse(Console.ReadLine(), out numberOfNewWords))
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                    enteredCorrectly = false;
                }
            } while (!enteredCorrectly);

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
                    Console.WriteLine("A - Eingabe Abbrechen");
                    Console.WriteLine("Ihre erste eingabe lautet:\n");

                    //Output languages and input words
                    foreach (var language in languageArray)
                    {
                        Console.Write(language.PadRight(20, ' '));
                    }
                    Console.WriteLine();
                    foreach (var word in newTranslations)
                    {
                        Console.Write(word.PadRight(20, ' '));
                    }
                    Console.WriteLine();
                    do
                    {
                        //user decide if input is correct
                        string choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "W": enteredCorrectly = false; validInput = true; break;
                            case "w": enteredCorrectly = false; validInput = true; break;
                            case "E": enteredCorrectly = true; validInput = true; break;
                            case "e": enteredCorrectly = true; validInput = true; break;
                            case "A": Console.Clear(); return; 
                            case "a": Console.Clear(); return; 

                            default:
                                Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                                validInput = false;
                                break;
                        }
                    } while (!validInput);

                } while (!enteredCorrectly);

                //creating new translation and add new Translation to the list
                Translator translation = new Translator(newTranslations, languageArray);
                try
                {
                    vocabularyList.AddNewWordsToList(translation);
                    vocabularyList.AddWordsToCSV(filePath, newTranslations);
                }
                catch (Exception )
                {
                    Console.WriteLine("Der Dienst ist momentan nicht verfügbar..");
                }               
            }

            //Asking for other new words
            Console.WriteLine("\nWollen sie noch weitere Wörter hinzufügen schreiben sie ja");
            Console.WriteLine("Ansonsten beliebige Taste und enter drücken");
            if (Console.ReadLine() == "ja" || Console.ReadLine() == "Ja")
            {
                AddTranslation(vocabularyList);
                Console.Clear();
            }
            Console.Clear();
        }


                                
        static void VocabularyTest(VocabularyManager vocabularyList)
        {
            int userLang1;
            int userLang2;
            int choice;
            string tippWord;
            bool successChoice;
            bool successLanguage;
            bool translationSuccess;

            //decision which language to learn 
            Console.WriteLine("Bitte beachten Sie die Groß- und Kleinschreibung der eingegebenen Wörter\n");
            Console.WriteLine("Welche Sprache soll gelernt werden?");

            //show existing languages in VocabularyList
            string[] translationArray = vocabularyList.GetLanguages();
            for (int i = 0; i < translationArray.Length; i++)
            {                
                Console.Write("{0} {1} ", i+1, translationArray[i]);               
            }             
            
            //select the first language by Index
            Console.WriteLine("\nIndex der ersten Sprache");
            do
            {
                successLanguage = true;

                if (!int.TryParse(Console.ReadLine(), out userLang1) || userLang1 > translationArray.Length || userLang1 < 0)
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                    successLanguage = false;
                }
            } while (!successLanguage);

            //select the second language by Index
            Console.WriteLine("\nIndex der zweiten Sprache");
            do
            {
                successLanguage = true;

                if (!int.TryParse(Console.ReadLine(), out userLang2) || userLang2 > translationArray.Length || userLang2 < 0)
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                    successLanguage = false;
                }
                if (userLang2==userLang1)
                {
                    Console.WriteLine("Sie können nicht zweimal den die Selbe Sprache auswählen. Bitte Widerholen!");
                    successLanguage = false;
                }
            } while (!successLanguage);            


            //choose the translation direction 
            int languageIndex1=userLang1-1;    
            int languageIndex2=userLang2-1;

            //choose direction of query
            Console.WriteLine("In welche Sprache möchten Sie übersetzen?\n1.{0}->{1} oder 2.{1}->{0}", translationArray[languageIndex1], translationArray[languageIndex2]);            
            do
            {
                successChoice = true;               
                if(!int.TryParse(Console.ReadLine(),out choice))
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                    successChoice = false;
                }       
                if(choice !=1 && choice != 2)
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                    successChoice = false;
                }
            } while (!successChoice);                                         
            
            //select order
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

            //translation programm
            int counterRightWords = 0;
            string[] collectionWords = new string[2];
            string randomWord;
            string randomWordTransl; 

            translationSuccess = true;
            for (int i = 0; i < 10; i++)
            {
                //get a RandomWord and the right translation for training 
                Console.Clear();
                collectionWords = vocabularyList.RandomWord(languageIndex1, languageIndex2);
                randomWord = collectionWords[0];
                randomWordTransl = collectionWords[1];
                Console.WriteLine();
                Console.WriteLine(randomWord);
                Console.WriteLine("Übersetzen Sie das Wort in {0}", translationArray[languageIndex2]);

                //For counting the wrong inputs 
                int tippNumber = 0;
                //translation from User
                string inputWord = Console.ReadLine(); 
                do
                {
                    string placeHolder = "";
                    translationSuccess = true;        
                    //Checking input from user
                    bool inputRight = vocabularyList.CheckingTranslation(randomWord, inputWord, languageIndex1, languageIndex2);
                    if (inputRight)
                    {
                        //correct Translation
                        Console.WriteLine("Wort ist richtig. Enter -> nächstes Wort");
                        Console.ReadLine();
                        counterRightWords++;
                        translationSuccess = true;
                    }
                    else
                    {                          
                        //wrong Translation -> Tip 
                        Console.WriteLine("Das Wort ist falsch.");
                        //if the word is less than 3 he will not get any tips
                        if (randomWordTransl.Length >= 3) 
                        {                           
                            tippNumber++;
                            //first letters of the word as tip
                            tippWord = randomWordTransl.Remove(randomWordTransl.Length - (randomWordTransl.Length - tippNumber));
                            for (int j = 0; j < randomWordTransl.Length - tippNumber; j++)
                            {
                                placeHolder += " _";
                            }
                            Console.WriteLine("Hier ein Tipp: {0}{1} -> Versuchen Sie es erneut", tippWord, placeHolder);
                            inputWord = Console.ReadLine();                            
                            inputRight = vocabularyList.CheckingTranslation(randomWord, inputWord, languageIndex1, languageIndex2);
                        }                           
                        if (inputRight)
                        {
                            Console.WriteLine("Wort ist richtig. Enter -> nächstes Wort");
                            Console.ReadLine();
                            counterRightWords++;
                            translationSuccess = true;
                        }
                        //if the word is wrong after the third input it is considered wrong
                        else if(tippNumber == 2)
                        {
                            Console.WriteLine("Das Wort ist falsch.");
                            Console.ReadLine();
                            translationSuccess = true;
                        }
                        else
                        {
                            translationSuccess = false;
                        }
                    }                    
                } while (!translationSuccess);                
            }   
            //Result
            Console.WriteLine("Sie haben {0} von 10 Wörter richtig\n", counterRightWords);
            Console.ReadLine();
            Console.Clear();
        }
    }
}

