using System;
using System.Collections.Generic;

namespace Trivia;

public class Players
{
    private readonly List<Player> _players = new();
    private int _currentPlayer;
    public string currentPlayerName => getCurrentPlayer().ToString();

    public Player getCurrentPlayer()
    {
        return _players[_currentPlayer];
    }

    public bool Add(string playerName)
    {
        _players.Add(new Player(playerName));

        Console.WriteLine(playerName + " was added");
        Console.WriteLine("They are player number " + _players.Count);
        return true;
    }

    public int PlayersCount()
    {
        return _players.Count;
    }

    public void NextPlayerTurn()
    {
        if (++_currentPlayer == _players.Count) _currentPlayer = 0;
    }
}