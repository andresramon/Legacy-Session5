using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;
        private readonly Questionare _questionare;
        private readonly Players _players;

        public Game()
        {
            _questionare = new Questionare();
            _players = new Players(this);
        }

        public String currentPlayer => _players._players[_currentPlayer];

        public void AddPlayer(string name)
        {
            _players.Add(name);
        }

        public bool IsPlayable()
        {
            return (_players.PlayersCount() >= 2);
        }

        public void Roll(int roll)
        {
            Console.WriteLine(currentPlayer + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_players._inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(currentPlayer + " is getting out of the penalty box");
                    _players.MoveCurrentPlayer(roll, _currentPlayer);
                                     
                    _questionare.AskQuestion(_players.CurrentCategory(_currentPlayer));
                }
                else
                {
                    Console.WriteLine(currentPlayer + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _players.MoveCurrentPlayer(roll, _currentPlayer);
                _questionare.AskQuestion(_players.CurrentCategory(_currentPlayer));
            }
        }

        public bool WasCorrectlyAnswered()
        {
            if (_players._inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    _players.CorrectlyAnswered(_currentPlayer);

                    var winner = DidPlayerWin();
                    NextPlayerTurn();

                    return winner;
                }
                else
                {
                    NextPlayerTurn();
                    return true;
                }
            }
            else
            {
                _players.CorrectlyAnswered(_currentPlayer);

                var winner = DidPlayerWin();
                NextPlayerTurn();

                return winner;
            }
        }

        private void NextPlayerTurn()
        {
            _currentPlayer++;
            if (_currentPlayer == _players._players.Count) _currentPlayer = 0;
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(currentPlayer + " was sent to the penalty box");
            _players._inPenaltyBox[_currentPlayer] = true;

            NextPlayerTurn();
            return true;
        }


        private bool DidPlayerWin()
        {
            return !(_players._purses[_currentPlayer] == 6);
        }
    }

}
