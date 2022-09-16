using System.Collections.Generic;

namespace Trivia;

public class Questionare
{
    public readonly LinkedList<string> _popQuestions = new LinkedList<string>();
    public readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
    public readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
    public readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

    public Questionare()
    {
        InitQuestionare();
    }

    public void InitQuestionare()
    {
        for (var i = 0; i < 50; i++)
        {
            this._popQuestions.AddLast("Pop Question " + i);
            this._scienceQuestions.AddLast(("Science Question " + i));
            this._sportsQuestions.AddLast(("Sports Question " + i));
            this._rockQuestions.AddLast("Rock Question " + i);
        }
    }
}