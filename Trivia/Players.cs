using System;
using System.Collections.Generic;

namespace Trivia;

public class Players
{
    private int _currentPlayer=0;
    private readonly List<Player> _players = new List<Player>();

    public Player getCurrentPlayer() => _players[_currentPlayer];
    public String currentPlayerName => getCurrentPlayer().ToString();
    
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

    public void NextPlayerTurn()
    {
        if (++_currentPlayer == this._players.Count) _currentPlayer = 0;
    }
}