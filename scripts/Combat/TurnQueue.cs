using System;
using Godot;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


public class TurnQueue {
	public List<Combatant> CurrentTurnOrder;
	
	public TurnQueue(List<Combatant> list) {
		CurrentTurnOrder = list.OrderByDescending(o=>o.Data.Agility).ToList();
	}

	public void PrintOrder() {
		for (int i = 0; i < CurrentTurnOrder.Count; i++) {
			Debug.Print(CurrentTurnOrder.ElementAt(i).Data.Agility.ToString());
		}
	}
	
}
