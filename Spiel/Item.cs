using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Spiel
{
    // Basis-Klasse für alle Items
    public class Item
    {
        public string name = "";
        public string beschreibung = "";
        public int maxStack = 1;
    }

    // Basis-Klasse für alle Schwerter
    public class Schwert : Item
    {
        public int attackDamage = 0;
        public int durabilty = 50;
    }

    // Basis-Klasse für alle Zaubertränke
    public class Potion : Item
    {
        PotionEffect effect = new PotionEffect();
    }

    // Basis-Klasse für alle Zauber-Effekte
    public class PotionEffect
    {
        public virtual void UsePotion()
        {
            "Es wurde eine Basis-Potion ohne Effekt verwendet.".ThrowError();
        }
    }
}
