using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia;

public class Questionnaire : Publisher
{


    
    private readonly LinkedList<string> _popQuestions;
    private readonly LinkedList<string> _rockQuestions;
    private readonly LinkedList<string> _scienceQuestions;
    private readonly LinkedList<string> _sportsQuestions;

    public Questionnaire(int numberOfQuestions)
    {
        _popQuestions = CreateQuestionsList(Categories.Pop, numberOfQuestions);
        _scienceQuestions = CreateQuestionsList(Categories.Science, numberOfQuestions);
        _sportsQuestions = CreateQuestionsList(Categories.Sports, numberOfQuestions);
        _rockQuestions = CreateQuestionsList(Categories.Rock, numberOfQuestions);
    }

    public LinkedList<string> CreateQuestionsList(Categories category, int numberOfQuestions)
    {
        var questions = new LinkedList<string>();

        for (var i = 0; i < numberOfQuestions; i++) questions.AddLast($"{category} Question " + i);

        return questions;
    }

    public void AskQuestion(Categories currentCategory)
    {
        var questionsList = GetListByCategory(currentCategory);

        Raise("The category is " + currentCategory);
        Raise(questionsList.First());

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