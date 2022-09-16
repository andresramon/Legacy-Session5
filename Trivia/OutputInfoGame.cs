using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia
{
    public class OutputInfoGame
    {
        public virtual void DisplayLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
