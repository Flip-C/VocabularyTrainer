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
            //das file exist müsste man noch abfangen falls false
            bool success = File.Exists(filePath);
            bool inputend = true;
            
            VocabularyManager manager = new VocabularyManager();
            manager.CsvParser(filePath);

            do
            {
                //Kürzere Einführung (L...Sprachen) übersichtlicher...
                //Willkommen und mögliche Funktionen 
                Console.WriteLine("Willkommen zum Vokabeltrainer");
                Console.WriteLine("Ihnen stehen folgende Funktionen zur Verfügung\n1. Sie können abfragen welche Sprachen das Programm beinhaltet\n2. Das Programm kann Sie auf 10 unterschiedliche Vokabeln der ausgewählten Sprache prüfen und zählt Ihren Fortschritt.\n3. Wenn Sie schon ein Profi in den bekannten Vokabeln sind, können Sie neue Wörter hinzufügen und sich weiter trainieren\n");
                Console.WriteLine("Für die möglichen Sprachen dürcken Sie bitte >L<\nUm der Liste ein neues Wort anzuhängen drücken sie bitte >A<\nUm auf 10 Vokabeln geprüft zu werden drücken Sie bitte >T<\nUm das Programm zu beenden drücken Sie bitte >E<");

                string input = Console.ReadLine();

                //Auswahl für groß und kleinbuchstaben...
                //inputEnd ...übersichtlicher...
                //bei falscher eingabe nicht ganz zum anfang hüpfen...
                switch (input)
                {
                    case "a": AddTranslation(manager); inputend = true; break;
                    case "t": VocabularyTest(manager); inputend = true; break;
                    case "e": inputend = false; break;
                    default:
                        inputend = false;
                        Console.WriteLine("Falsche Eingabe");
                        break;
                }
            } while(!inputend);  
        }

        //Allgemein:
        //mit manager.irgendwas kannst du dann auf die funktionen im vocabularyManager zugreifen. 
        //also z.b manager.AddNewWord etc... die wird allerdings erst dann von dir aufgerufen, wenn 
        //die ganze userinteraktion abgeschlossen ist, sprich alle wörter in einem gültigen Format eingegeben worden sind


        //Funktion in VocabularyManager (manager.GetTranslationLanguage) aufrufen, die die aktuellen sprachen in einem Array zurück gibt 
        //Array[0] = erste Sprache / Array[1] = zweite Sprache usw...
        //wie viele Wörter abgefragt werden hängt davon ab wie groß das Array ist. -> Warum abfrage !!Frage von Philipp 
        //WriteLine "Das neue Wort in {erster Übersetzungsprache} eingeben" ->überprüfen - dann input vom User
        //WriteLine "Das neue Wort in {zweiter Übersetzungsprache} eingeben" ->überprüfen - dann input vom User
        //.... das ganze bist die "Array.Lenght"'s Sprache eingegeben wurde
        //dann ein array mit den übersetzungen an manager.AddNewWords zurück geben
        static void AddTranslation(VocabularyManager manager)
        {
            //evt. GetTranslationLanguages einmal aufrufen am anfang und in ein lokas array speichern...
            //dann kann man Console.WriteLine mit for schleife ausgeben und spart sich code und ist flexibel was daher kommt...
            //nach jedem Console.WriteLine ein ReadLine und die wörter auch in array speichern. ...

            bool succesadd = true;
            //do
            //{                
                Console.WriteLine("\nEs stehen die Sprachen:\n1.'{0}' 2.'{1}' 3.'{2}' 4.'{3}' zur Verfügung", manager.GetTranslationLanguages());              
                

                Console.WriteLine("Geben Sie das Wort in {0} ein", manager.GetTranslationLanguages());
                

                Console.WriteLine("Geben Sie das Wort in {1} ein", manager.GetTranslationLanguages());
               

                Console.WriteLine("Geben Sie das Wort in {2} ein", manager.GetTranslationLanguages());


                Console.WriteLine("Geben Sie das Wort in {3} ein", manager.GetTranslationLanguages());

            //...vorher noch abfragen wie viele wörter er hinzufügen will, dann muss man nicht immer neu starten das ganze...

            //} while (succesadd);                  
        }



        //Abfrage vom User von Welcher auf welche sprache er abgefragt werden möchte
        //Auswahl der sprachen legt das languagesArray fest welches du mit manager.GetTranslationLanguages bekommst 
        //er wählt zwei sprachen aus und wählt dann die Richtung z.B deutsch -> englisch oder englisch -> deutsch
        //er bekommt 10 Vokabeln und danach wird zusammengefasst z.b 8/10 richtig... (counting variable oder so anlegen die mitzählt)
        //mit manager.RandomWord(Übergabe von Index der ersten Sprache) bekommst du ein Wort zurück welches dann ausgegeben wird
        //Beim checken mit  "CheckingTranslation" übergibst du einerseits das random wort als erstes, dann das vom user eingegebene wort
        //dann den Index von der ersten Sprache ( die sprache die das Random wort hat) und Index von Sprache in die übersetzt wurde


        
        //Wie gehts gescheit ins Auswhlmenü zurück? -> von wo?..

        static void VocabularyTest(VocabularyManager manager)
        {
            bool trainingsuccess = true;                 

            int firstLanguageIndex = 0; //Deutsch hat Indizes 0
            int secLanguageIndex = 1; //Englisch hat Indizes 1
            int thirdLanguageIndex = 2; //Französisch hat Indizes 2
            int fourthLanguageIndex = 3; //Italienisch  hat Indizes 3
            
            //...mit foreach das LanguageArray ausgeben -> flexibel egal wie viel sprachen kommen...
            Console.WriteLine("\nIn welcher Sprache möchten Sie die Vokabeln trainieren?\n1.{1} 2.{2} 3.{3} -> Geben Sie die zugehörige Zahl ein", manager.GetTranslationLanguages());
            
            //...tryparse verweden und evtl Fehler bei angabe abfangen...
            //...zwei Console.ReadLine weil er ja von einer sprache in eine andere übersetzt, wir brauchen also immer 2 entscheidungen
            //...vom User...
            //...dann noch user fragen wie die reihenfolge sein soll... also 1->2 oder 2->1 !!Bis dahin ist es uns egal was er ausgewählt hat!
            //...wir müssen mit indizes arbeiten und nicht mit den sprachen die wir jetzt mal gemacht haben also deutsch englisch etc...
            //...später kann 1 auch eine komplett andere sprache sein!...
            int auswahl=int.Parse(Console.ReadLine());


            //Deutsch zu Englisch //Habe immer die LanguageIndex geändert also Deutsch 0 und Englisch 1
            if (auswahl == 1)
            {
                //...denglisch? :D ...
                //...immer counterRight schreiben, liest sich viel besser wenn man das Programm nicht kennt!...
                int counterwdh = 0;
                int counterright = 0;            
            
                for (int i = 0; i < 10; i++)
                {
                    string comparingWord = manager.RandomWord(firstLanguageIndex);
                    Console.WriteLine();
                    Console.WriteLine(comparingWord);
                    //...Da würde dann eben immer der indizes kommen, den der User ausgewählt hat...
                    //...wenn er z.b von deutsch -> englisch dann das lokale sprachenarray an der stelle[1] (da steht dann englisch)...
                    Console.WriteLine("Übersetzen Sie das Wort in {1}", manager.GetTranslationLanguages());

                    string inputWord = Console.ReadLine(); //Lösung vom User

                    //...war nur ein Prototyp von mir,evtl success1 noch sprechender umbenennen...
                    //...Der first- und secIndex wird vom user eingegeben und gespeichert...
                    //übergabe mit unten angeführten Daten / return dann true oder false
                    bool success1 = manager.CheckingTranslation(comparingWord, inputWord, firstLanguageIndex, secLanguageIndex);
                    if (success1)
                    {
                        Console.WriteLine("Wort ist richtig");
                        counterright++;
                    }
                    else
                    {
                        Console.WriteLine("Wort ist falsch");
                    }


                    //...für was ist das?...
                    //...du kannst auch mit i arbeiten, sparst du dir eine variable und das ++...
                    //...evtl fragst du vorher wie viele Vokabeln er bekommen möchte und lässt die schleife so oft durchlaufen
                    //...und unten statt 10 steht dann der input vom user...
                    //...ich schreib dir noch eine Methode wo du dem user sagen kannst am anfang wie viele wörter zur verfügung stehen....
                    counterwdh ++;
                    if (counterwdh == 10)
                    {
                        trainingsuccess = false;
                    }
                    
                }
                Console.WriteLine("Sie haben {0} von 10 Wörter richtig\n", counterright);
            }           	       
            

            //...Der rest ist dann hinfällig weil wir eh flexibel sind und nicht für jede Sprache eine eigene else if brauchen...

            //Deutsch zu Französisch Index 0 zu 2
            else if (auswahl==2)
            {
                 int counterwdh = 0;
                int counterright = 0;
            
            
                for (int i = 0; i < 10; i++)
                {
                    string comparingWord = manager.RandomWord(firstLanguageIndex);
                    Console.WriteLine();
                    Console.WriteLine(comparingWord);
                    Console.WriteLine("Übersetzen Sie das Wort in {2}", manager.GetTranslationLanguages());

                    string inputWord = Console.ReadLine(); //Lösung vom User

                    //übergabe mit unten angeführten Daten / return dann true oder false
                    bool success1 = manager.CheckingTranslation(comparingWord, inputWord, firstLanguageIndex, thirdLanguageIndex);
                    if (success1)
                    {
                        Console.WriteLine("Wort ist richtig");
                        counterright++;
                    }
                    else
                    {
                        Console.WriteLine("Wort ist falsch");
                    }
                    counterwdh ++;
                    if (counterwdh == 10)
                    {
                        trainingsuccess = false;
                    }
                    
                }
                Console.WriteLine("Sie haben {0} von 10 Wörter richtig\n", counterright);
            }       
             
            //Deutsch zu Italienisch Index 0 zu 3
            else if (auswahl==3)
            {
                 int counterwdh = 0;
                int counterright = 0;
            
            
                for (int i = 0; i < 10; i++)
                {
                    string comparingWord = manager.RandomWord(firstLanguageIndex);
                    Console.WriteLine();
                    Console.WriteLine(comparingWord);
                    Console.WriteLine("Übersetzen Sie das Wort in {3}", manager.GetTranslationLanguages());

                    string inputWord = Console.ReadLine(); //Lösung vom User

                    //übergabe mit unten angeführten Daten / return dann true oder false
                    bool success1 = manager.CheckingTranslation(comparingWord, inputWord, firstLanguageIndex, fourthLanguageIndex);
                    if (success1)
                    {
                        Console.WriteLine("Wort ist richtig");
                        counterright++;
                    }
                    else
                    {
                        Console.WriteLine("Wort ist falsch");
                    }
                    counterwdh ++;
                    if (counterwdh == 10)
                    {
                        trainingsuccess = false;
                    }
                    
                }
                Console.WriteLine("Sie haben {0} von 10 Wörter richtig\n", counterright);
            }       
                                            
        }
    }
}
