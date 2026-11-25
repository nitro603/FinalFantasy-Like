using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


public class AutoBattler {
    private List<Combatant> Opponents;
    
    public AutoBattler(List<Combatant> currentOpponents) {
        Opponents = currentOpponents;
    }

    public BattleAction Choose() {
        Random rng = new Random();
        int index = rng.Next(0,Opponents.Count);
        List<Combatant> targets = new List<Combatant>([Opponents.ElementAt(index)]);
        return new BattleAction(0, targets);
    }
    
    
   
}