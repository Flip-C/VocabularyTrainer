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
            bool success = File.Exists(filePath);
            
            VocabularyManager manager = new VocabularyManager();
            manager.CsvParser(filePath);

            //hier trifft der User seine Allgemeine Entscheidung und springt dann je nachdem ind die Mehode unten
            //z.B AddTranslation etc... wenn ausgeführt kommt er wieder ins Main und kann nochmal entscheiden Bzw beenden..



            //die Nächsten paar zeilen habe ich versucht das "überprüfen" aus zu probieren
            //wenn du den text unten bis zeile 41 auskommentierst sollte das eigentlich funktionieren
            //Habs auch nochmal kommentiert 

            //string comparingWord = "Baum"; //Random Wort
            //string inputWord = "tree"; //Lösung vom User
            //int firstLanguageIndex = 0; //Deutsch hat Indizes 0
            //int secLanguageIndex = 1; //Englisch hat Indizes 0

            //übergabe mit unten angeführten Daten / return dann true oder false
            //bool success1 = manager.CheckingTranslation(comparingWord, inputWord,firstLanguageIndex,secLanguageIndex);
            //if (success1)
            //{
            //    Console.WriteLine("Wort ist richtig");
            //}
            //else
            //{
            //    Console.WriteLine("Wort ist falsch");
            //}







            //Aufruf der unten stehenden Methoden Wichtig ist das du den manager übergibst
            VocabularyTest(manager);
            AddTranslation(manager);
        }

        //Allgemein:
        //mit manager.irgendwas kannst du dann auf die funktionen im vocabularyManager zugreifen. 
        //also z.b manager.AddNewWord etc... die wird allerdings erst dann von dir aufgerufen, wenn 
        //die ganze userinteraktion abgeschlossen ist, sprich alle wörter in einem gültigen Format eingegeben worden sind





        //Funktion in VocabularyManager (manager.GetTranslationLanguage) aufrufen, die die aktuellen sprachen in einem Array zurück gibt 
        //Array[0] = erste Sprache / Array[1] = zweite Sprache usw...
        //wie viele Wörter abgefragt werden hängt davon ab wie groß das Array ist. 
        //WriteLine "Das neue Wort in {erster Übersetzungsprache} eingeben" ->überprüfen - dann input vom User
        //WriteLine "Das neue Wort in {zweiter Übersetzungsprache} eingeben" ->überprüfen - dann input vom User
        //.... das ganze bist die "Array.Lenght"'s Sprache eingegeben wurde
        //dann ein array mit den übersetzungen an manager.AddNewWords zurück geben
        static void AddTranslation(VocabularyManager manager)
        {
           
        }



        //Abfrage vom User von Welcher auf welche sprache er abgefragt werden möchte
        //Auswahl der sprachen legt das languagesArray fest welches du mit manager.GetTranslationLanguages bekommst 
        //er wählt zwei sprachen aus und wählt dann die Richtung z.B deutsch -> englisch oder englisch -> deutsch
        //er bekommt 10 Vokabeln und danach wird zusammengefasst z.b 8/10 richtig... (counting variable oder so anlegen die mitzählt)
        //mit manager.RandomWord(Übergabe von Index der ersten Sprache) bekommst du ein Wort zurück welches dann ausgegeben wird
        //Beim checken mit  "CheckingTranslation" übergibst du einerseits das random wort als erstes, dann das vom user eingegebene wort
        //dann den Index von der ersten Sprache ( die sprache die das Random wort hat) und Index von Sprache in die übersetzt wurde
        static void VocabularyTest(VocabularyManager manager)
        {
            
        }
    }
}
