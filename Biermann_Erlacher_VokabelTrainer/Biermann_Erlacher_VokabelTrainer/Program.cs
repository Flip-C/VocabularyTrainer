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
            string choice = null;
            bool inputEnd = true;
            bool fileExists = File.Exists(filePath);
            VocabularyManager vocabularyList = new VocabularyManager();

            if (!fileExists)
            {
                Console.WriteLine("Die Datei befindet sich nicht im Angegebenen Ordner");
                Console.WriteLine("Beliebige Taste zum Beenden drücken");
                Console.ReadLine();
                return;
            }

            //Parse vocabularyList from CSV
            //Exception wenn Datei nicht lesbar, z.b offen etc
            //Funktioniert leider noch nicht ganz...
            do
            {
                int check = vocabularyList.CsvParser(filePath);
                if (check == 0)
                {
                    Console.WriteLine("Die Datei konnte nicht gelesen werden. Überprüfen sie ob sie die Datei nicht geöffnet haben");
                    Console.WriteLine("A - drücken um es erneut zu versuchen");
                    Console.WriteLine("Zum Beenden drücken Sie Enter");
                    choice = Console.ReadLine();
                    if (!(choice=="A")&&!(choice=="a"))
                    {
                        return;
                    }
                }
            } while (choice == "A" || choice=="a");

            Console.WriteLine("Die Datei wurde soeben erfolgreich eingelesen");
            

            do
            {
                //Kürzere Einführung (L...Sprachen) übersichtlicher...
                //Willkommen und mögliche Funktionen 
                Console.WriteLine("Willkommen zum Vokabeltrainer");
                Console.WriteLine("Ihnen stehen folgende Funktionen zur Verfügung\n1. Sie können abfragen welche Sprachen das Programm beinhaltet\n2. Das Programm kann Sie auf 10 unterschiedliche Vokabeln der ausgewählten Sprache prüfen und zählt Ihren Fortschritt.\n3. Wenn Sie schon ein Profi in den bekannten Vokabeln sind, können Sie neue Wörter hinzufügen und sich weiter trainieren\n");
                Console.WriteLine("Für die möglichen Sprachen dürcken Sie bitte >L<\nUm der Liste ein neues Wort anzuhängen drücken sie bitte >A<\nUm auf 10 Vokabeln geprüft zu werden drücken Sie bitte >T<\nUm das Programm zu beenden drücken Sie bitte >E<");

                string inputChoice = Console.ReadLine();

                //Auswahl für groß und kleinbuchstaben...
                //inputEnd ...übersichtlicher...
                //bei falscher eingabe nicht ganz zum anfang hüpfen...
                //...nach while schleife abfrage ob programm beendet werden soll...
                switch (inputChoice)
                {
                    case "a": AddTranslation(vocabularyList); inputEnd = true; break;
                    case "A": AddTranslation(vocabularyList); inputEnd = true; break;
                    case "t": VocabularyTest(vocabularyList); inputEnd = true; break;
                    case "T": VocabularyTest(vocabularyList); inputEnd = true; break;
                    case "e": inputEnd = false; break;
                    default:
                        inputEnd = false;
                        Console.WriteLine("Falsche Eingabe");
                        break;
                }
            } while (!inputEnd);
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


            int firstLanguageIndex = 0; //Deutsch hat Indizes 0
            int secLanguageIndex = 1; //Englisch hat Indizes 1


            string[] translationArray = vocabularyList.GetLanguages();

            for (int i = 0; i < translationArray.Length; i++)
            {
                Console.Write("{0}", translationArray[i]);
            }

            //...tryparse verweden und evtl Fehler bei angabe abfangen...
            //...zwei Console.ReadLine weil er ja von einer sprache in eine andere übersetzt, wir brauchen also immer 2 entscheidungen
            //...dann noch user fragen wie die reihenfolge sein soll... also 1->2 oder 2->1 !!Bis dahin ist es uns egal was er ausgewählt hat!
            //...wir müssen mit indizes arbeiten und nicht mit den sprachen die wir jetzt mal gemacht haben also deutsch englisch etc...
            //...später kann 1 auch eine komplett andere sprache sein!...
            int auswahl = int.Parse(Console.ReadLine());
            Console.WriteLine("zweite sprache");
            int auswahl2 = int.Parse(Console.ReadLine());

            /*if (auswahl1 -> auswahl2)
            {

            }
            else(auswahl2 -> auswahl1)
            {

            }
            */

            int counterright = 0;

            for (int i = 0; i < 10; i++)
            {
                string comparingWord = vocabularyList.RandomWord(firstLanguageIndex);
                Console.WriteLine();
                Console.WriteLine(comparingWord);
                //...Da würde dann eben immer der indizes kommen, den der User ausgewählt hat...
                //...wenn er z.b von deutsch -> englisch dann das lokale sprachenarray an der stelle[1] (da steht dann englisch)...
                Console.WriteLine("Übersetzen Sie das Wort in {1}", translationArray[secLanguageIndex]);

                string inputWord = Console.ReadLine(); //Lösung vom User

                bool success1 = vocabularyList.CheckingTranslation(comparingWord, inputWord, firstLanguageIndex, secLanguageIndex);
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

