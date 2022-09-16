using System;
using System.Collections.Generic;
using System.Linq;
namespace Trivia;

public class Questionare
{
    private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
    private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
    private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
    private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

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

    public void AskQuestion(string currentCategory)
    {
        if (currentCategory == "Pop")
        {
            Console.WriteLine(_popQuestions.First());
            this._popQuestions.RemoveFirst();
        }
        if (currentCategory == "Science")
        {
            Console.WriteLine(this._scienceQuestions.First());
            this._scienceQuestions.RemoveFirst();
        }
        if (currentCategory == "Sports")
        {
            Console.WriteLine(this._sportsQuestions.First());
            this._sportsQuestions.RemoveFirst();
        }
        if (currentCategory == "Rock")
        {
            Console.WriteLine(this._rockQuestions.First());
            this._rockQuestions.RemoveFirst();
        }
    }
}