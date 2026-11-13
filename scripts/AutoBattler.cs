using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Final_Fantasy_Like.scripts;

public class AutoBattler
{
    private List<Combatant> Opponents;
    
    public AutoBattler(List<Combatant> currentOpponents)
    {
        Opponents = currentOpponents;
    }

    public BattleAction Choose()
    {
        Random rng = new Random();
        int index = rng.Next(0,Opponents.Count);
        
        return new BattleAction(Opponents.ElementAt(index).Data.Name, "Attack");
    }
    
    
   
}