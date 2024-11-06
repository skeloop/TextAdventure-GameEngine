using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Dialog
    {
        public static void LoadDialog()
        {
            textDictonary.Add("einleitung", new List<string>()
            {
                "Es scheint als ob du überhaupt nix besitzt...",
                "Wir verwenden hier Hauptsächlich Schwerter, Bögen und Zaubertränke.",
                "Naja vielleicht auch mal eine Ak-47... Wer weiß?",
                "Aber fangen wir erstmal langsam an.",
                "So wie in jedem Spiel musst auch DU dich hier hoch arbeiten!",
                "Deswegen überreiche ich dir mit Stolz dieses...",
                "...",
                "Holzschwert!, TA DA!",
                "* du nimmst den Stock und schaust ihn dir an *",
            });
        }

        public static Dictionary<string, List<string>> textDictonary = new Dictionary<string, List<string>>();

        public static void Play(string dialog_id, int playTimeInMilliseconds = 0)
        {
            if (textDictonary.TryGetValue(dialog_id, out var list))
            {
                foreach(var text in list)
                {
                    text.Print();
                    Thread.Sleep(playTimeInMilliseconds);
                }
            } else
            {

                ("Es wurde versucht auf einen Dialog zu zugreifen der nicht existiert!" +
                    $"\n   >> Benutzte ID: {dialog_id} | Wert: 'null'").ThrowError();
                
            }
        }
    }
}
