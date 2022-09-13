﻿using System.Collections;
using System.Collections.Generic;
using Trivia;

namespace Tests
{

    public class TestableGame : Game
    {
        public List<string> consoleText = new List<string>();

        protected override void DisplayLine(string text)
        {
            consoleText.Add(text);
        }

        public void ClearConsoleText()
        {
            consoleText.Clear();
        }
    }
}