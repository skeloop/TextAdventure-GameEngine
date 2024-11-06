using System;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using GameEngine.Spiel;
namespace GameEngine
{
    internal static class Program
    {
        /// <summary>
        /// Erweiterung => Gibt einen string(Text) direkt in der Konsole aus
        /// </summary>
        /// <param name="text"></param>
        public static void Print(this string text, ConsoleColor consoleColor = ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void Wait(this int miliSeconds)
        {
            Thread.Sleep(miliSeconds);
        }

        public static void ThrowError(this string errorText)
        {
            Console.WriteLine("\n[FEHLER] => " + errorText);
            Console.WriteLine("\n           Drücke eine Taste um fortzufahren... \n");
            Console.ReadKey();
        }


        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!DynamicCompiler.LoadExternalAssembly())
            {
                "Um das Spiel zu starten müssen erst alle Fehler behoben werden.".ThrowError();
                return;
            } else
            {
                "Mods wurden erfolgreich geladen.".Print(ConsoleColor.DarkGreen);
                "Spiel starten...".Print(ConsoleColor.DarkGreen);
                2500.Wait();
                Console.Clear();
            }

            InitGame();
            GameLoop();
        }

        // Code der vor Spielstart benutzt wird
        static void InitGame()
        {
            Dialog.LoadDialog();
            spieler.inventar.InitInventar();
        }

        // Code für die allgemeine Spiellogik
        public static Spieler spieler = new Spieler();

        public static bool running = false;
        static void GameLoop()
        {
            running = true;

            spieler.name = Terminal.GetUserTextInput("Hallo! Willkommen beim Text Adventure.\nBitte wähle einen Namen:");
            $"Freut mich dich kennen zu lernen, {spieler.name}".Print();
            Dialog.Play("einleitung");
            spieler.inventar.AddItemToInventory(new Item() { name = "Holzknüppel", beschreibung = "Eine moosige Waffe" });

            while (running)
            {
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.E)
                {
                    spieler.inventar.ShowInventar();
                }
            }
        }

        private static void StartForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }



    }
}
