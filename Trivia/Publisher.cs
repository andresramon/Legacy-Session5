using System;

namespace Trivia;

public class Publisher
{
    public Action<string> OnChange { get; set; }

    public void Raise(string message)
    {
        if (OnChange != null)
        {
            OnChange(message);
        }
    }
}