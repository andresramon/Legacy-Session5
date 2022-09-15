using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<string> _players = new List<string>();

        private readonly int[] _places = new int[6];
        private readonly int[] _purses = new int[6];

        protected readonly bool[] _inPenaltyBox = new bool[6];

        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        protected int _currentPlayer;
        protected bool _isGettingOutOfPenaltyBox;
        protected const int _numMinPlayers = 2;
        protected const int _numMaxQuestions = 50;
        
        public Game()
        {
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

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= _numMinPlayers);
        }

        public bool Add(string playerName)
        {
            InitializePlayer(playerName);

            DisplayInitializePlayer(playerName);
            return true;
        }

        private void DisplayInitializePlayer(string playerName)
        {
            DisplayLine(playerName + " was added");
            DisplayLine("They are player number " + _players.Count);
        }

        private void InitializePlayer(string playerName)
        {
            _players.Add(playerName);
            _places[HowManyPlayers()] = 0;
            _purses[HowManyPlayers()] = 0;
            _inPenaltyBox[HowManyPlayers()] = false;
        }

        public int HowManyPlayers()
        {
            return _players.Count;
        }

        public void Roll(int roll)
        {
            DisplayCurrentPlayerRoll(roll);

            if (IsCurrentPlayerInPenaltyBox())
            {
                if (IsEvenRoll(roll))
                {
                    _isGettingOutOfPenaltyBox = true;

                    DisplayLine(_players[_currentPlayer] + " is getting out of the penalty box");
                    MoveCurrentPlayerToNewLocation(roll);
                    AskQuestion();
                    return;
                }
                
                DisplayLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                _isGettingOutOfPenaltyBox = false;
                return;
            }
            
            MoveCurrentPlayerToNewLocation(roll);
            AskQuestion();
        }

        private bool IsCurrentPlayerInPenaltyBox()
        {
            return _inPenaltyBox[_currentPlayer];
        }

        private void DisplayCurrentPlayerRoll(int roll)
        {
            DisplayLine(_players[_currentPlayer] + " is the current player");
            DisplayLine("They have rolled a " + roll);
        }

        private void MoveCurrentPlayerToNewLocation(int roll)
        {
            _places[_currentPlayer] = _places[_currentPlayer] + roll;
            if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

            DisplayMovementCurrentPlayer();
        }

        private void DisplayMovementCurrentPlayer()
        {
            DisplayLine(_players[_currentPlayer]
                        + "'s new location is "
                        + _places[_currentPlayer]);
            DisplayLine("The category is " + CurrentCategory());
        }

        private static bool IsEvenRoll(int roll)
        {
            return roll % 2 != 0;
        }


        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                AskQuestionCategory(_popQuestions);
                return;
            }
            if (CurrentCategory() == "Science")
            {
                AskQuestionCategory(_scienceQuestions);
                return;
            }
            if (CurrentCategory() == "Sports")
            {
                AskQuestionCategory(_sportsQuestions);
                return;
            }
            if (CurrentCategory() == "Rock")
            {
                AskQuestionCategory(_rockQuestions);
                return;
            }
        }

        private void AskQuestionCategory(LinkedList<string> questionList)
        {
            DisplayLine(questionList.First());
            questionList.RemoveFirst();
        }

        private string CurrentCategory()
        {
            if (IsPopCategory())
            {
                return "Pop";
            }

            if (IsScienceCategory())
            {
                return "Science";
            }

            if (IsSportsCategory())
            {
                return "Sports";
            }

            return "Rock";
        }

        private bool IsSportsCategory()
        {
            return _places[_currentPlayer] == 2 || _places[_currentPlayer] == 6 || _places[_currentPlayer] == 10;
        }

        private bool IsScienceCategory()
        {
            return _places[_currentPlayer] == 1 || _places[_currentPlayer] == 5 || _places[_currentPlayer] == 9;
        }

        private bool IsPopCategory()
        {
            return _places[_currentPlayer] == 0 || _places[_currentPlayer] == 4 || _places[_currentPlayer] == 8;
        }

        public bool WasCorrectlyAnswered()
        {
            if (IsCurrentPlayerInPenaltyBox() && !_isGettingOutOfPenaltyBox)
            {
                ChangeToNextPlayer();
                return true;
            }
            
            return CheckIfWinnerWithCorrectAnswer();
        }

        private bool CheckIfWinnerWithCorrectAnswer()
        {
            DisplayCorrectAnswerAndIncrementsCurrentCoins();
            var winner = DidPlayerWin();
            ChangeToNextPlayer();
            return winner;
        }

        private void DisplayCorrectAnswerAndIncrementsCurrentCoins()
        {
            DisplayLine("Answer was correct!!!!");
            _purses[_currentPlayer]++;
            DisplayLine(_players[_currentPlayer]
                    + " now has "
                    + _purses[_currentPlayer]
                    + " Gold Coins.");
        }

        private void ChangeToNextPlayer()
        {
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
        }

        public bool WrongAnswer()
        {
            DisplayLine("Question was incorrectly answered");
            DisplayLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return !(_purses[_currentPlayer] == 6);
        }

        protected virtual void DisplayLine(string text)
        {
            Console.WriteLine(text);
        }
    }

}
