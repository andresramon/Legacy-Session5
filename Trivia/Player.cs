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

    public int GetLocation()
    {
        return location;
    }
}