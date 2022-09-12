using System;
using Trivia;

namespace Test;

public class TestableGame:Game
{
    public string consoleText = "";
    
    protected override void DisplayLine(string text)
    {
        consoleText += text + (". ");
    }
}