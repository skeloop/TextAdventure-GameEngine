using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Spiel
{
    public class Inventar
    {
        // Die Anzahl an Slots die im Inventar vorhanden sind
        static int slotAnzahl = 12;

        // Eine Liste mit 12 Slots
        public List<Slot> slots = new List<Slot>();

        public void InitInventar()
        {
            for(int i = 0; i < slotAnzahl; i++)
            {
                slots.Add(new Slot());
            }
        }


        // Funktion um Items dem Inventar hinzu zu fügen
        public void AddItemToInventory(Item zielItem)
        {
            foreach(var slot in slots)
            {
                // Zuerst überprüfen ob der Spieler bereits ein Item vom selben Typ hat...
                if (slot.item.name == zielItem.name)
                {
                    // Falls ja: Überprüfen ob die maximale Stack Anzahl erreicht wurde.
                    if (slot.anzahl < zielItem.maxStack)
                    {
                        // Erhöhe die anzahl des Items im Slot um 1
                        slotAnzahl++;
                    }
                }

                // Falls der Spieler dieses Item noch nicht hat muss der erste leere Slot gefunden werden
                if (slot.item.name == "" || slot.anzahl == 0)
                { 
                    // Falls ja: Füge das Item in den Slot hinzu
                    slot.item = zielItem;
                    slot.anzahl++;
                    $"\n >> Du hast ein Item aufgesammelt: {zielItem.name}\n".Print(ConsoleColor.DarkCyan);
                    return;
                }
            }

            $"Du hast kein Platz mehr in deinem Inventar für: {zielItem.name}".Print(ConsoleColor.DarkRed);
        }

        // Funktion um das Inventar in der Konsole auszugeben
        public void ShowInventar()
        {
            Console.Clear();
            "Dein Inventar:\n\n".Print();
            foreach (var slot in slots)
            {
                if (slot.item.name == "")
                {
                    continue;
                }

                $"{slot.item.name}".Print();
                $"{slot.item.beschreibung}".Print();
                "---------=====================--------".Print();
            }
        }
    }


    public class Slot
    {
        // Jeder Slot kann 1 Item-Typ haben.
        // Standardwert ist ein leeres 'Item-Objekt' ohne Name, Beschreibung etc... da jeder Slot bei Spielstart leer sein muss.
        public Item item = new Item();
        public int anzahl = 0;
    }
}
