using System;
using System.Diagnostics;

namespace Final_Fantasy_Like.scripts;

public class AutoBattler
{
    private Combatant[] Opponents;
    
    public AutoBattler(Combatant[] currentOpponents)
    {
        Opponents = currentOpponents;
    }

    public Combatant Choose()
    {
        Random rng = new Random();
        int number = rng.Next(1, 101);
        
        Debug.Print(number.ToString());
        return new Combatant();
    }
    
    
   
}