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

    public void MoveCurrentPlayer(int roll, int currentPlayer)
    {
        this._places[currentPlayer] = this._places[currentPlayer] + roll;
        if (_places[currentPlayer] > 11) _places[currentPlayer] = _places[currentPlayer] - 12;

        Console.WriteLine(_players[currentPlayer] + "'s new location is " + _places[currentPlayer]);

    }

    public string CurrentCategory(int currentPlayer)
    {
        int playersPlace = _places[currentPlayer];
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

    public void CorrectlyAnswered(int player)
    {
        Console.WriteLine("Answer was correct!!!!");
        this._purses[player]++;
        Console.WriteLine(_players[player]
                          + " now has "
                          + this._purses[player]
                          + " Gold Coins.");
    }
}