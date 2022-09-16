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

        protected readonly QuestionsPackage questions;
        protected readonly OutputInfoGame outputDevice;

        protected int _currentPlayer;
        protected bool _isGettingOutOfPenaltyBox;
        protected const int _numMinPlayers = 2;
        
        
        public Game()
        {
            outputDevice = new OutputInfoGame();
            questions = new QuestionsPackage(outputDevice);
        }

        public string CreateRockQuestion(int index)
        {
            return questions.CreateRockQuestion(index);
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
                if (!IsEvenRoll(roll))
                {
                    SetGettingOutOfPenaltyBox(false);
                    return;
                }

                SetGettingOutOfPenaltyBox(true);
            }

            MoveCurrentPlayerToNewLocation(roll);
            questions.AskQuestion(_places[_currentPlayer]);
        }

        private void SetGettingOutOfPenaltyBox(bool gettingOutOfPenaltyBox)
        {
            _isGettingOutOfPenaltyBox = gettingOutOfPenaltyBox;

            if (gettingOutOfPenaltyBox)
            {
                DisplayLine(_players[_currentPlayer] + " is getting out of the penalty box");
            }
            else
            {
                DisplayLine(_players[_currentPlayer] + " is not getting out of the penalty box");
            }
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
            DisplayLine("The category is " + questions.CurrentCategory(_places[_currentPlayer]));
        }

        private static bool IsEvenRoll(int roll)
        {
            return roll % 2 != 0;
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
            DisplayWrongAnswer();
            SetPlayerInPenaltyBox(_currentPlayer);
            ChangeToNextPlayer();
            return true;
        }

        private void DisplayWrongAnswer()
        {
            DisplayLine("Question was incorrectly answered");
            DisplayLine(_players[_currentPlayer] + " was sent to the penalty box");
        }

        private bool DidPlayerWin()
        {
            return !(_purses[_currentPlayer] == 6);
        }

        protected virtual void DisplayLine(string text)
        {
            outputDevice.DisplayLine(text);
        }

        protected void SetPlayerInPenaltyBox(int numPlayer)
        {
            _inPenaltyBox[numPlayer] = true;
        }
    }

}
