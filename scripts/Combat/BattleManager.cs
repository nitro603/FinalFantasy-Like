using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Final_Fantasy_Like.scripts;

public partial class BattleManager : Node {
	private TurnQueue Queue;
	private List<Combatant> Team;
	private List<Combatant> Enemies;
	
	public override void _Ready() {
		var combatants = FindChildren("*", "Node", true, false).OfType<Combatant>().ToList();
		
		Team = new List<Combatant>();
		Enemies = new List<Combatant>();
		GroupCombatants(combatants);
		SetOpponents();
		
		Queue = new TurnQueue(combatants);
		//StartRound();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		
	}

	public void StartBattle() {
		
	}

	public void StartRound() {
		MakeEnemyDecisions();
		foreach (var combatant in Queue.CurrentTurnOrder) {
			String target = combatant.Decision.Target;
			int damage = combatant.Data.AttackDmg;
			bool enemy = combatant.Data.Enemy;
			
			Debug.Print("Combatant (" + combatant.Data.Name + ") attacks " + target + " for " + damage + " Attack Points");
			BasicAttack(combatant.Decision.Target, combatant.Data.AttackDmg, combatant.Data.Enemy);
			//check Status
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

	public void CheckStatus(Combatant combatant, bool enemy) {
		if (enemy) {
			if (combatant.Dead) {
				
			}	
		}
		else {
			if (combatant.Dead) {
				Team.ElementAt(Team.IndexOf(combatant)).QueueFree();
			}
		}
	}

	public List<Combatant> GetTeam() {
		return Team;
	}
	
	private void MakeEnemyDecisions() {
		foreach (var member in Enemies) {
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
		foreach (var member in Enemies) {
			member.SetAI(new AutoBattler(Team));
		}
	}
}
