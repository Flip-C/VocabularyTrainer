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

            Console.WriteLine("Die Datei wurde soeben erfolgreich eingelesen");


            bool checkChoice = true;
            Console.WriteLine("Willkommen zum Vokabeltrainer");
            Console.WriteLine("Um der Liste ein neues Wort anzuhängen drücken sie bitte >A<");
            Console.WriteLine("Um auf 10 Vokabeln geprüft zu werden drücken Sie bitte >T<");
            Console.WriteLine("Um das Programm zu beenden drücken Sie bitte >E<");
            do
            {
                string inputChoice = Console.ReadLine();
                //Auswahl für groß und kleinbuchstaben...
                //inputEnd ...übersichtlicher...
                //bei falscher eingabe nicht ganz zum anfang hüpfen...
                //...nach while schleife abfrage ob programm beendet werden soll...
                switch (inputChoice)
                {
                    case "a": AddTranslation(vocabularyList); checkChoice = true; break;
                    case "A": AddTranslation(vocabularyList); checkChoice = true; break;
                    case "t": VocabularyTest(vocabularyList); checkChoice = true; break;
                    case "T": VocabularyTest(vocabularyList); checkChoice = true; break;


                    default:
                        checkChoice = false;
                        Console.WriteLine("Falsche Eingabe, bitte Buchstaben eingeben");
                        break;
                }
            } while (!checkChoice);

            //Console.WriteLine("Soll Programm beendet werden? J/N");
            //string end = Console.ReadLine();
            //    switch(end)
            //    {
            //        case "J": inputEnd = false; break;
            //        case "N": inputEnd = true; break;
            //    }
            
            //} while (!inputEnd);
        }

        static void AddTranslation(VocabularyManager vocabularyList)
        {
            string[] languageArray = vocabularyList.GetLanguages();
            string[] newTranslations = new string[languageArray.Length];
            int numberOfNewWords;
            bool success = true;
            bool switchSuccess = true;

            //asking for number of new words
            Console.WriteLine("Wie viele neue Wörter wollen sie hinzufügen");
            do
            {
                success = true;
                if (!int.TryParse(Console.ReadLine(), out numberOfNewWords))
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte Wiederholen");
                    success = false;
                }
            } while (!success);

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

                    //user decide if input is correct
                    string choice = Console.ReadLine();
                    do
                    {
                        switch (choice)
                        {
                            case "W": success = false; break;
                            case "w": success = false; break;
                            case "E": success = true; break;
                            case "e": success = true; break;
                            default:
                                Console.WriteLine("Ungültige Eingabe. Bitte Widerholen");
                                switchSuccess = false;
                                break;
                        }
                    } while (!switchSuccess);

                } while (!success);

                //creating new translation and add new Translation to the list
                Translator translation = new Translator(newTranslations, languageArray);
                vocabularyList.AddNewWordsToList(translation);
                vocabularyList.AddWordsToCSV(filePath, newTranslations);
            }
            Console.WriteLine("\nWollen sie noch weitere Wörter hinzufügen schreiben sie ja");
            Console.WriteLine("Ansonsten beliebige Taste und enter drücken");
            if (Console.ReadLine() == "ja" || Console.ReadLine() == "Ja");
            {
                AddTranslation(vocabularyList);
            }
        }




        //Abfrage vom User von Welcher auf welche sprache er abgefragt werden möchte
        //Auswahl der sprachen legt das languagesArray fest welches du mit manager.GetTranslationLanguages bekommst 
        //er wählt zwei sprachen aus und wählt dann die Richtung z.B deutsch -> englisch oder englisch -> deutsch
        //er bekommt 10 Vokabeln und danach wird zusammengefasst z.b 8/10 richtig... (counting variable oder so anlegen die mitzählt)
        //mit manager.RandomWord(Übergabe von Index der ersten Sprache) bekommst du ein Wort zurück welches dann ausgegeben wird
        //Beim checken mit  "CheckingTranslation" übergibst du einerseits das random wort als erstes, dann das vom user eingegebene wort
        //dann den Index von der ersten Sprache ( die sprache die das Random wort hat) und Index von Sprache in die übersetzt wurde
        //Wie gehts gescheit ins Auswhlmenü zurück? -> von wo?..

        
        static void VocabularyTest(VocabularyManager vocabularyList)
        {
            Console.WriteLine("Welche Sprache soll gelernt werden?");
            string[] translationArray = vocabularyList.GetLanguages();
            for (int i = 0; i < translationArray.Length; i++)
            {                
                Console.Write("{0} {1} ", i+1, translationArray[i]);               
            }


            //...tryparse verweden und evtl Fehler bei angabe abfangen...
            //...zwei Console.ReadLine weil er ja von einer sprache in eine andere übersetzt, wir brauchen also immer 2 entscheidungen
            //...dann noch user fragen wie die reihenfolge sein soll... also 1->2 oder 2->1 !!Bis dahin ist es uns egal was er ausgewählt hat!
            //...wir müssen mit indizes arbeiten und nicht mit den sprachen die wir jetzt mal gemacht haben also deutsch englisch etc...
            //...später kann 1 auch eine komplett andere sprache sein!...


            //Überprüfen ob eingegebene zahl auch exestiert bzw der index -> sonst haben wir gleich einen IndexOutofRange
            Console.WriteLine("\nErste Sprache");
            int trainLang1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Zweite Sprache");
            int trainLang2 = int.Parse(Console.ReadLine());

            int languageIndex1=trainLang1-1;   //hier soll die Sprache im Array ausgewählt 
            int languageIndex2=trainLang2-1;   //-1 weil dem user ja der index +1 angezeigt wird und ausgewählt wird

            //!!!!!!!!!!!!!!!!!!!!!!!ACHTUNG LÖSUNG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //!!!!!!!!!!!!!!!!!!!!!!!ACHTUNG LÖSUNG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //Lösung für das nächste Console.WirteLine :P
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!--------------->------------>Console.WriteLine("1.{0} -> {1} \n2.{1} -> {0}", translationArray[languageIndex1], translationArray[languageIndex2]);
            //!!!!!!!!!!!!!!!!!!!!!!!ACHTUNG LÖSUNG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //!!!!!!!!!!!!!!!!!!!!!!!ACHTUNG LÖSUNG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            Console.WriteLine("In welche Sprache möchten Sie übersetzen?\n 1.{0}->{1} oder 2.{3}->{4}",trainLang1,trainLang2,trainLang2,trainLang1);
            int choice = int.Parse(Console.ReadLine());


            //Prinzipiell schon sehr gut, nur warum machst du nicht nur die zuordnung in eine if else, dann musst du nicht den ganzen code
            //2 mal schreiben, je nachdem was für eine Reihenfolge er wählt

            if (choice == 1)
            {
                languageIndex1 = trainLang1;
                languageIndex2 = trainLang2;

                int counterRightWords = 0;

                for (int i = 0; i < 10; i++)
                {
                    string comparingWord = vocabularyList.RandomWord(languageIndex1);
                    Console.WriteLine();
                    Console.WriteLine(comparingWord);
                    //...Da würde dann eben immer der indizes kommen, den der User ausgewählt hat...
                    //...wenn er z.b von deutsch -> englisch dann das lokale sprachenarray an der stelle[1] (da steht dann englisch)...
                    Console.WriteLine("Übersetzen Sie das Wort in {1}", translationArray[languageIndex2]);

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
            else                
            {
                languageIndex1 = trainLang2;
                languageIndex2 = trainLang1;
                int counterright = 0;

                for (int i = 0; i < 10; i++)
                {
                    string comparingWord = vocabularyList.RandomWord(languageIndex2);
                    Console.WriteLine();
                    Console.WriteLine(comparingWord);
                    //...Da würde dann eben immer der indizes kommen, den der User ausgewählt hat...
                    //...wenn er z.b von deutsch -> englisch dann das lokale sprachenarray an der stelle[1] (da steht dann englisch)...
                    Console.WriteLine("Übersetzen Sie das Wort in {1}", translationArray[languageIndex1]);

                    string inputWord = Console.ReadLine(); //Lösung vom User

                    bool success1 = vocabularyList.CheckingTranslation(comparingWord, inputWord, languageIndex2, languageIndex1);
                    if (success1)
                    {
                        Console.WriteLine("Wort ist richtig");
                        counterright++;
                    }
                    else
                    {
                        Console.WriteLine("Wort ist falsch");
                    }

                }
                Console.WriteLine("Sie haben {0} von 10 Wörter richtig\n", counterright);
            }
            

           

        }
    }
}

