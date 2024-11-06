using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Terminal
    {
        public static string GetUserTextInput(string text = "")
        {
            if (text != "") Console.WriteLine(text);
            return Console.ReadLine();
        }
    }
}
