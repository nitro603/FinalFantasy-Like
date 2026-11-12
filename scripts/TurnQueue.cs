using System;
using Godot;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


public class TurnQueue 
{
	
	public List<Combatant> CurrentTurnOrder;

	public TurnQueue(Combatant[] _team, Combatant[] _enemies)
	{
		//Array.Sort(list, (a, b) => b.Data.Agility.CompareTo(a.Data.Agility)); 
		//CurrentTurnOrder = list.ToList();
	}

	public void FuckShit()
	{
		Combatant character1 = CurrentTurnOrder.ElementAt(0);
		Combatant character2 = CurrentTurnOrder.ElementAt(1);
		
		Debug.Print("Testing Combat");
		Debug.Print("Current Stats");
		Debug.Print("Reina " + character1.CurrentHp);
		Debug.Print("Cricko " + character2.CurrentHp);
		if (character1.Data.Agility > character2.Data.Agility)
		{
			character2.TakeDamage(character1.Data.AttackDmg);
		}
		else
		{
			character1.TakeDamage(character2.Data.AttackDmg);
		}
	}
	public void PrintOrder()
	{
		for (int i = 0; i < CurrentTurnOrder.Count; i++)
		{
			Debug.Print(CurrentTurnOrder.ElementAt(i).Data.Agility.ToString());
		}
	}
	
}
