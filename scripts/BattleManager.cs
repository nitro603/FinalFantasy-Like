using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Final_Fantasy_Like.scripts;

public partial class BattleManager : Node {
	private TurnQueue Queue;
	private List<Combatant> Team;
	private List<Combatant> Enemies;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		var combatants = FindChildren("*", "Node", true, false).OfType<Combatant>().ToList();
		//change to list, in order to work with groupCombatants function
		
		Team = new List<Combatant>();
		Enemies = new List<Combatant>();
		GroupCombatants(combatants);
		SetOpponents();
		MakeDecisions();
		
		Queue = new TurnQueue(combatants);
		//StartRound();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		
	}

	public void StartRound() {
		foreach (var combatant in Queue.CurrentTurnOrder) {
			String target = combatant.Decision.Target;
			int damage = combatant.Data.AttackDmg;
			bool enemy = combatant.Data.Enemy;
			
			Debug.Print("Combatant (" + combatant.Data.Name + ") attacks " + target + " for " + damage + " Attack Points");
			BasicAttack(combatant.Decision.Target, combatant.Data.AttackDmg, combatant.Data.Enemy);
		}
	}

	public void BasicAttack(String name, int attackDamage, bool enemy) {
		if (enemy) {
			foreach (var opponent in Team) {
				if (opponent.Data.Name == name) {
					CalculationResult result = DamageCalculator.basicAttack(attackDamage, opponent.Data.Defense);
					opponent.TakeDamage(result.Damage);
				}
			}
		}
		else {
			foreach (var opponent in Enemies) {
				if (opponent.Data.Name == name) {
					CalculationResult result = DamageCalculator.basicAttack(attackDamage, opponent.Data.Defense);
					opponent.TakeDamage(result.Damage);
				}
			}
		}
	}
	
	private void MakeDecisions() {
		foreach (var member in Enemies) {
			member.GenerateDecision();
		}
		//remove when we get battleUIManager
		foreach (var member in Team) {
			member.GenerateDecision();
		}
	}
	
	private void GroupCombatants(List<Combatant> list) {
		foreach (var combatant in list) {
			if (combatant.Data.Enemy) {
				Enemies.Add(combatant);
			}
			else {
				Team.Add(combatant);
			}
		}
	}
	
	private void SetOpponents() {
		//remove when you enable BattleUIManager 
		foreach (var member in Team) {
			member.SetAI(new AutoBattler(Enemies));
		}
		foreach (var member in Enemies) {
			member.SetAI(new AutoBattler(Team));
		}
	}
}
