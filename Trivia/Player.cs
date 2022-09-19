using System;

namespace Trivia;

public class Player : Publisher
{
    private readonly string _name;
    private bool inPenaltyBox;
    private int location;
    private int purse;

    public Player(string name)
    {
        _name = name;
        location = 0;
        purse = 0;
        inPenaltyBox = false;
    }

    public override string ToString()
    {
        return _name;
    }

    public void MovePlayer(int roll)
    {
        location = location + roll;
        if (location > 11) location = location - 12;

        Raise(_name + "'s new location is " + location);
    }

    public void CorrectlyAnswered()
    {
        Raise("Answer was correct!!!!");
        purse++;
        Raise(_name
              + " now has "
              + purse
              + " Gold Coins.");
    }

    public bool DidPlayerWin()
    {
        return !(purse == 6);
    }

    public int GetLocation()
    {
        return location;
    }

    public bool IsInPenaltyBox()
    {
        return inPenaltyBox;
    }

    public void MoveToPenaltyBox()
    {
        Raise(_name + " was sent to the penalty box");
        inPenaltyBox = true;
    }

    public Categories GetCategory()
    {
        var playersPlace = GetLocation();
        return playersPlace switch
        {
            0 or 4 or 8 => Categories.Pop,
            1 or 5 or 9 => Categories.Science,
            2 or 6 or 10 => Categories.Sports,
            _ => Categories.Rock
        };
    }
}