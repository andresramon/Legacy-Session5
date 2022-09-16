using System;
using System.Collections.Generic;

namespace Trivia;

public class Players
{
    private Game _game;
    public readonly List<string> _players = new List<string>();
    public readonly int[] _places = new int[6];
    public readonly int[] _purses = new int[6];
    public readonly bool[] _inPenaltyBox = new bool[6];

    public Players(Game game)
    {
        _game = game;
    }

    public bool Add(string playerName)
    {
        _players.Add(playerName);
        _places[_game.Players._players.Count] = 0;
        _purses[_game.Players._players.Count] = 0;
        _inPenaltyBox[_game.Players._players.Count] = false;

        Console.WriteLine(playerName + " was added");
        Console.WriteLine("They are player number " + _players.Count);
        return true;
    }

    public int PlayersCount()
    {
        return this._players.Count;
    }
}