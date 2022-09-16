using System;
using System.Collections.Generic;
using System.Linq;
namespace Trivia;

public class Questionare
{
    public enum Categories
    {
        Pop,
        Science,
        Sports,
        Rock
    }
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
            this._popQuestions.AddLast($"{Categories.Pop} Question " + i);
            this._scienceQuestions.AddLast($"{Categories.Science} Question " + i);
            this._sportsQuestions.AddLast($"{Categories.Sports} Question " + i);
            this._rockQuestions.AddLast($"{Categories.Rock} Question " + i);
        }
    }

    public void AskQuestion(Categories currentCategory)
    {
        
        var questionsList = GetListByCategory(currentCategory);
        
        Console.WriteLine("The category is " + currentCategory);
        Console.WriteLine(questionsList.First());
        
        questionsList.RemoveFirst();
        
    }

    private LinkedList<string> GetListByCategory(Categories currentCategory)
    {
        LinkedList<string> questionsList;
        switch (currentCategory)
        {
            case Categories.Pop:
                questionsList = _popQuestions;
                break;
            case Categories.Science:
                questionsList = _scienceQuestions;
                break;
            case Categories.Sports:
                questionsList = _sportsQuestions;
                break;
            case Categories.Rock:
                questionsList = _rockQuestions;
                break;
            default:
                questionsList = _rockQuestions;
                break;
        }

        return questionsList;
    }
}