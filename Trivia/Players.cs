using System;
using System.Collections.Generic;

namespace Trivia;

public class Players
{
    private int _currentPlayer=0;

    private readonly List<Player> _players = new List<Player>();


    public Player currentPlayer => _players[_currentPlayer];
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
        int playersPlace = currentPlayer.GetLocation();
        if (playersPlace == 0) return "Pop";
        if (playersPlace == 4) return "Pop";
        if (playersPlace == 8) return "Pop";
        if (playersPlace == 1) return "Science";
        if (playersPlace == 5) return "Science";
        if (playersPlace == 9) return "Science";
        if (playersPlace == 2) return "Sports";
        if (playersPlace == 6) return "Sports";
        if (playersPlace == 10) return "Sports";
        return "Rock";
    }

    public void CorrectlyAnswered()
    {
        currentPlayer.CorrectlyAnswered();
    }

    public void NextPlayerTurn()
    {
        _currentPlayer++;
        if (_currentPlayer == this._players.Count) _currentPlayer = 0;
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