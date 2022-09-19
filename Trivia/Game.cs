using System;

namespace Trivia;

public class Game
{
    private readonly Players _players;
    private readonly Questionnaire _questionnaire;
    private bool _isGettingOutOfPenaltyBox;
    private Printer _printer = new Printer();
    public Game()
    {
        _questionnaire = new Questionnaire(50);
        _questionnaire.OnChange += message => _printer.Print(message);
       
        _players = new Players();
        _players.OnChange += message => _printer.Print(message);
    }

    private Player currentPlayer => _players.getCurrentPlayer();

    public void AddPlayer(string name)
    {
        var newPlayer = _players.Add(name);
        newPlayer.OnChange += s => _printer.Print(s);
    }

    public bool IsPlayable()
    {
        return _players.PlayersCount() >= 2;
    }


    public void Roll(int roll)
    {
        var playerName = _players.currentPlayerName;

        _printer.Print(playerName + " is the current player");
        _printer.Print("They have rolled a " + roll);

        if (currentPlayer.IsInPenaltyBox())
        {
            if (IsEvenRoll(roll))
            {
                _printer.Print(playerName + " is not getting out of the penalty box");
                _isGettingOutOfPenaltyBox = false;
                return;
            }

            _isGettingOutOfPenaltyBox = true;

            _printer.Print(playerName + " is getting out of the penalty box");
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
        _printer.Print("Question was incorrectly answered");
        currentPlayer.MoveToPenaltyBox();
        _players.NextPlayerTurn();
        return true;
    }

    private bool IsEvenRoll(int roll)
    {
        return roll % 2 == 0;
    }
}