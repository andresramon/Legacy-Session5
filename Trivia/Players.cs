using System;
using System.Collections.Generic;

namespace Trivia;

public class Players
{
    private int _currentPlayer=0;

    private readonly List<Player> _players = new List<Player>();
    private readonly int[] _places = new int[6];
    private readonly int[] _purses = new int[6];
    private readonly bool[] _inPenaltyBox = new bool[6];


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
        Console.WriteLine("Answer was correct!!!!");
        this._purses[_currentPlayer]++;
        Console.WriteLine(currentPlayerName
                          + " now has "
                          + this._purses[_currentPlayer]
                          + " Gold Coins.");
    }

    public void NextPlayerTurn()
    {
        _currentPlayer++;
        if (_currentPlayer == this._players.Count) _currentPlayer = 0;
    }

    public bool IsCurrentPlayerInPenaltyBox()
    {
        return this._inPenaltyBox[_currentPlayer];
    }

    public void SetCurrentPlayerInPenaltyBox()
    {
        Console.WriteLine(currentPlayerName + " was sent to the penalty box");
        _inPenaltyBox[_currentPlayer] = true;
    }

    public bool DidPlayerWin()
    {
        return !(this._purses[_currentPlayer] == 6);
    }
}