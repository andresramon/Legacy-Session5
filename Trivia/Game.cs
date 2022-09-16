using System;

namespace Trivia;

public class Game
{
    private readonly Players _players;
    private readonly Questionnaire _questionnaire;

    private bool _isGettingOutOfPenaltyBox;

    public Game()
    {
        _questionnaire = new Questionnaire(50);
        _players = new Players();
    }

    private Player currentPlayer => _players.getCurrentPlayer();

    public void AddPlayer(string name)
    {
        _players.Add(name);
    }

    public bool IsPlayable()
    {
        return _players.PlayersCount() >= 2;
    }


    public void Roll(int roll)
    {
        var playerName = _players.currentPlayerName;

        Console.WriteLine(playerName + " is the current player");
        Console.WriteLine("They have rolled a " + roll);

        if (currentPlayer.IsInPenaltyBox())
        {
            if (IsEvenRoll(roll))
            {
                Console.WriteLine(playerName + " is not getting out of the penalty box");
                _isGettingOutOfPenaltyBox = false;
                return;
            }

            _isGettingOutOfPenaltyBox = true;

            Console.WriteLine(playerName + " is getting out of the penalty box");
        }

        currentPlayer.MovePlayer(roll);
        _questionnaire.AskQuestion(currentPlayer.GetCategory());
    }


    public bool WasCorrectlyAnswered()
    {
        if (currentPlayer.IsInPenaltyBox() && !_isGettingOutOfPenaltyBox)
        {
            _players.NextPlayerTurn();
            return true;
        }

        currentPlayer.CorrectlyAnswered();
        var winner = currentPlayer.DidPlayerWin();
        _players.NextPlayerTurn();

        return winner;
    }

    public bool WrongAnswer()
    {
        Console.WriteLine("Question was incorrectly answered");
        currentPlayer.MoveToPenaltyBox();
        _players.NextPlayerTurn();
        return true;
    }

    private bool IsEvenRoll(int roll)
    {
        return roll % 2 == 0;
    }
}