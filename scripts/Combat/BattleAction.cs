using Godot;
using System;
using System.Collections.Generic;


public class BattleAction {
	
	public int Action;
	public List<Combatant> Targets;

	public BattleAction(int action, List<Combatant> targets) {
		Action = action;
		
		if (action == 0 || action == 1) {
			Targets = targets; 
		}
	}
	
	public BattleAction(int action) {
		Action = action;
	}
	
}
