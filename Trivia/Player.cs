using System;

namespace Trivia;

public class Player
{
    private readonly string _name;
    private int location;
    private int purse;
    private bool inPenaltyBox;
    
    public Player(String name)
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
        if (location> 11) location = location - 12;

        Console.WriteLine(_name + "'s new location is " + location);
    }

    public void CorrectlyAnswered()
    {
        Console.WriteLine("Answer was correct!!!!");
        purse++;
        Console.WriteLine(_name
                          + " now has "
                          + purse
                          + " Gold Coins.");
        
    }

    public bool DidPlayerWinn()
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
        Console.WriteLine(_name + " was sent to the penalty box");
        inPenaltyBox = true;
    }

    public string GetCategory()
    {
        int playersPlace = this.GetLocation();
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
}