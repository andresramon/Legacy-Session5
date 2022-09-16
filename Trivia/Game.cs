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
            var playerName = _players.currentPlayerName;
            
            Console.WriteLine(playerName + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_players.IsCurrentPlayerInPenaltyBox())
            {
                if (IsEvenRoll(roll))
                {
                    Console.WriteLine(playerName + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                    return;
                }
                else
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(playerName + " is getting out of the penalty box");
                }
            }
            
            _players.MoveCurrentPlayer(roll);
            _questionare.AskQuestion(_players.CurrentCategory());
        }

        private bool IsEvenRoll(int roll)
        {
            return roll % 2 == 0;
        }

        public bool WasCorrectlyAnswered()
        {
            if (_players.IsCurrentPlayerInPenaltyBox() && !_isGettingOutOfPenaltyBox)
            {
                _players.NextPlayerTurn();
                return true;
            }

            _players.CorrectlyAnswered();
            var winner = _players.DidPlayerWin();
            _players.NextPlayerTurn();

            return winner;
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
