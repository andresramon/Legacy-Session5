using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        
        private bool _isGettingOutOfPenaltyBox;
        private readonly Questionare _questionare;
        private readonly Players _players;

        public Game()
        {
            _questionare = new Questionare();
            _players = new Players();
        }

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
            Console.WriteLine(_players.currentPlayer + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_players.IsCurrentPlayerInPenaltyBox())
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players.currentPlayer + " is getting out of the penalty box");
                    _players.MoveCurrentPlayer(roll);
                                     
                    _questionare.AskQuestion(_players.CurrentCategory());
                }
                else
                {
                    Console.WriteLine(_players.currentPlayer + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _players.MoveCurrentPlayer(roll);
                _questionare.AskQuestion(_players.CurrentCategory());
            }
        }

        public bool WasCorrectlyAnswered()
        {
            if (_players.IsCurrentPlayerInPenaltyBox())
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    _players.CorrectlyAnswered();

                    var winner = _players.DidPlayerWin();
                    _players.NextPlayerTurn();

                    return winner;
                }
                else
                {
                    _players.NextPlayerTurn();
                    return true;
                }
            }
            else
            {
                _players.CorrectlyAnswered();

                var winner = _players.DidPlayerWin();
                _players.NextPlayerTurn();

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            _players.SetCurrentPlayerInPenaltyBox();
            _players.NextPlayerTurn();
            return true;
        }
    }

}
