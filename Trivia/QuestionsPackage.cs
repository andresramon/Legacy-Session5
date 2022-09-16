using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    public class QuestionsPackage
    {
        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();
        private readonly OutputInfoGame outputDevice;

        protected const int _numMaxQuestions = 50;

        public QuestionsPackage(OutputInfoGame outputDevice)
        {
            this.outputDevice = outputDevice;

            for (var i = 0; i < _numMaxQuestions; i++)
            {
                _popQuestions.AddLast(CreateCategoryQuestion("Pop", i));
                _scienceQuestions.AddLast(CreateCategoryQuestion("Science", i));
                _sportsQuestions.AddLast(CreateCategoryQuestion("Sports", i));
                _rockQuestions.AddLast(CreateCategoryQuestion("Rock", i));
            }
        }

        private string CreateCategoryQuestion(string Category, int index)
        {
            return Category + " Question " + index;
        }

        public string CreateRockQuestion(int index)
        {
            return CreateCategoryQuestion("Rock", index);
        }

        public void AskQuestion(int place)
        {
            if (CurrentCategory(place) == "Pop")
            {
                AskQuestionCategory(_popQuestions);
                return;
            }
            if (CurrentCategory(place) == "Science")
            {
                AskQuestionCategory(_scienceQuestions);
                return;
            }
            if (CurrentCategory(place) == "Sports")
            {
                AskQuestionCategory(_sportsQuestions);
                return;
            }
            if (CurrentCategory(place) == "Rock")
            {
                AskQuestionCategory(_rockQuestions);
            }
        }

        private void AskQuestionCategory(LinkedList<string> questionList)
        {
            outputDevice.DisplayLine(questionList.First());
            questionList.RemoveFirst();
        }

        public string CurrentCategory(int place)
        {
            if (IsPopCategory(place))
            {
                return "Pop";
            }

            if (IsScienceCategory(place))
            {
                return "Science";
            }

            if (IsSportsCategory(place))
            {
                return "Sports";
            }

            return "Rock";
        }

        private bool IsSportsCategory(int place)
        {
            return place == 2 || place == 6 || place == 10;
        }

        private bool IsScienceCategory(int place)
        {
            return place == 1 || place == 5 || place == 9;
        }

        private bool IsPopCategory(int place)
        {
            return place == 0 || place == 4 || place == 8;
        }
    }
}
