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

        public String currentPlayer => Players._players[_currentPlayer];
        
        public Players Players
        {
            get { return _players; }
        }

        public int CurrentPlayer
        {
            set { _currentPlayer = value; }
            get { return _currentPlayer; }
        }

        public bool IsPlayable()
        {
            return (Players.PlayersCount() >= 2);
        }

        public void Roll(int roll)
        {
            Console.WriteLine(currentPlayer + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (Players._inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(currentPlayer + " is getting out of the penalty box");
                    Players.MoveCurrentPlayer(roll, _currentPlayer);
                                     
                    _questionare.AskQuestion(Players.CurrentCategory(_currentPlayer));
                }
                else
                {
                    Console.WriteLine(currentPlayer + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                Players.MoveCurrentPlayer(roll, _currentPlayer);
                _questionare.AskQuestion(Players.CurrentCategory(_currentPlayer));
            }
        }

        public bool WasCorrectlyAnswered()
        {
            if (Players._inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Players.CorrectlyAnswered(_currentPlayer);

                    var winner = DidPlayerWin();
                    _currentPlayer++;
                    if (_currentPlayer == Players._players.Count) _currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == Players._players.Count) _currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                Players.CorrectlyAnswered(_currentPlayer);

                var winner = DidPlayerWin();
                _currentPlayer++;
                if (_currentPlayer == Players._players.Count) _currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(currentPlayer + " was sent to the penalty box");
            Players._inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == Players._players.Count) _currentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return !(Players._purses[_currentPlayer] == 6);
        }
    }

}
