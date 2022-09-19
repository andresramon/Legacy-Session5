using System;
using System.Collections.Generic;

namespace Trivia;

public class Players : Publisher
{
    private readonly List<Player> _players = new();
    private int _currentPlayer;

    public string currentPlayerName => getCurrentPlayer().ToString();

    public Player getCurrentPlayer()
    {
        return _players[_currentPlayer];
    }

    public Player Add(string playerName)
    {
        var player = new Player(playerName);
        _players.Add(player);

        Raise(playerName + " was added");
        Raise("They are player number " + _players.Count);
        
        return player;
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