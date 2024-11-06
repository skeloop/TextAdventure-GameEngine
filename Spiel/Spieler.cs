using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Spiel
{
    public class Spieler
    {
        // Darf am Anfang keinen Wert haben da wir nicht wissen wie der Spieler heißen wird.
        public string name = "";

        // Inventar
        public Inventar inventar = new Inventar();

        
    }
}
