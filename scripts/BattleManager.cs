using Godot;
using System;
using System.Diagnostics;
using System.Linq;

public partial class BattleManager : Node
{
	private TurnQueue _queue;
	private Combatant[] _team;
	private Combatant[] _enemies;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var combatants = FindChildren("*", "Node", true, false).OfType<Combatant>().ToArray();
		//Debug.Print(combatants.ElementAt(0).Data.Name);
		GroupCombatants(combatants);
		
		_queue = new TurnQueue(_team,_enemies);
		//_queue.PrintOrder();
		StartCombat();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void StartCombat()
	{
		_queue.FuckShit();
	}

	private void GroupCombatants(Combatant[] list)
	{
		
		for (int i = 0; i < list.Length; i++)
		{
			if (list[0].Data.Enemy)
			{
				_enemies.Append(list[i]);
			}
			else
			{
				_team.Append(list[i]);
			}
		}
	}
	
	private void SayHi()
	{
		Debug.Print("Hello World");
	}
}
