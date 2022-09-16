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

        String currentPlayer => Players._players[_currentPlayer];
        
        public Players Players
        {
            get { return _players; }
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        public int HowManyPlayers()
        {
            return Players._players.Count;
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
                    Players._places[_currentPlayer] = Players._places[_currentPlayer] + roll;
                    if (Players._places[_currentPlayer] > 11) Players._places[_currentPlayer] = Players._places[_currentPlayer] - 12;

                    Console.WriteLine(currentPlayer
                                      + "'s new location is "
                                      + Players._places[_currentPlayer]);
                    Console.WriteLine("The category is " + CurrentCategory());
                    _questionare.AskQuestion(CurrentCategory());
                }
                else
                {
                    Console.WriteLine(currentPlayer + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                Players._places[_currentPlayer] = Players._places[_currentPlayer] + roll;
                if (Players._places[_currentPlayer] > 11) Players._places[_currentPlayer] = Players._places[_currentPlayer] - 12;

                Console.WriteLine(currentPlayer
                                  + "'s new location is "
                                  + Players._places[_currentPlayer]);
                Console.WriteLine("The category is " + CurrentCategory());
                _questionare.AskQuestion(CurrentCategory());
            }
        }

        private string CurrentCategory()
        {
            if (Players._places[_currentPlayer] == 0) return "Pop";
            if (Players._places[_currentPlayer] == 4) return "Pop";
            if (Players._places[_currentPlayer] == 8) return "Pop";
            if (Players._places[_currentPlayer] == 1) return "Science";
            if (Players._places[_currentPlayer] == 5) return "Science";
            if (Players._places[_currentPlayer] == 9) return "Science";
            if (Players._places[_currentPlayer] == 2) return "Sports";
            if (Players._places[_currentPlayer] == 6) return "Sports";
            if (Players._places[_currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool WasCorrectlyAnswered()
        {
            if (Players._inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    Players._purses[_currentPlayer]++;
                    Console.WriteLine(currentPlayer
                                      + " now has "
                                      + Players._purses[_currentPlayer]
                                      + " Gold Coins.");

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
                Console.WriteLine("Answer was corrent!!!!");
                Players._purses[_currentPlayer]++;
                Console.WriteLine(currentPlayer
                                  + " now has "
                                  + Players._purses[_currentPlayer]
                                  + " Gold Coins.");

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
