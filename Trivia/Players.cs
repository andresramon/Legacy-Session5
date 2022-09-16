using System;
using System.Collections.Generic;

namespace Trivia;

public class Players
{
    private int _currentPlayer=0;
    private readonly List<Player> _players = new List<Player>();

    private Player currentPlayer => _players[_currentPlayer];
    public String currentPlayerName => currentPlayer.ToString();
    
    public bool Add(string playerName)
    {
        _players.Add(new Player(playerName));

        Console.WriteLine(playerName + " was added");
        Console.WriteLine("They are player number " + _players.Count);
        return true;
    }

    public int PlayersCount()
    {
        return this._players.Count;
    }

    public void MoveCurrentPlayer(int roll)
    {
        currentPlayer.MovePlayer(roll);
    }
 
    public string CurrentCategory()
    {
        return currentPlayer.GetCategory();
    }

    public void CorrectlyAnswered()
    {
        currentPlayer.CorrectlyAnswered();
    }

    public void NextPlayerTurn()
    {
        if (++_currentPlayer == this._players.Count) _currentPlayer = 0;
    }

    public bool IsCurrentPlayerInPenaltyBox()
    {
        return currentPlayer.IsInPenaltyBox();
    }

    public void SetCurrentPlayerInPenaltyBox()
    {
        currentPlayer.MoveToPenaltyBox();
    }

    public bool DidPlayerWin()
    {
        return currentPlayer.DidPlayerWinn();
    }
}