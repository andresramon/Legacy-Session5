using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivia;

namespace Tests
{
    public class TestableOutputInfoGame : OutputInfoGame
    {
        public List<string> consoleText = new List<string>();

        public override void DisplayLine(string text)
        {
            consoleText.Add(text);
        }

        public void ClearConsoleText()
        {
            consoleText.Clear();
        }
    }
}
