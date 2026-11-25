using Godot;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public partial class BattleManager : Node {
	private TurnQueue Queue;
	private List<Combatant> Team;
	private List<Combatant> Enemies;

	public override void _Ready() {
		var combatants = FindChildren("*", "Node", true, false).OfType<Combatant>().ToList();

		Team = new List<Combatant>();
		Enemies = new List<Combatant>();
		GroupCombatants(combatants);
		GD.Print(Team.ElementAt(0).Data.Name);
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
	   
	   foreach (var attacker in Queue.CurrentTurnOrder.ToList()) {
		  if (attacker.Decision.Action == 0) {
			 if (BattleOver())
				break;
	
			 if (attacker.Dead)
				continue;
	
			 var targets = ResolveTargets(attacker);
			 if (targets.Count == 0)   // ResolveTargets never returns null, so check for empty
				continue;

			 BasicAttack(attacker, targets);
		  }
		  
	   }

	   EndRound();
	}
	
	// Returns only targets that are still valid: alive and on the opposing side.
	// If every chosen target is already dead, it redirects to a living opponent.
	// The returned list never contains null, and is empty only when no opponents are left.
	private List<Combatant> ResolveTargets(Combatant attacker) {
	   var picked = attacker.Decision.Targets ?? new List<Combatant>();
	   var opponents = (attacker.Data.Enemy ? Team : Enemies).Where(o => !o.Dead).ToList();

	   var chosen = picked
		  .Where(t => t != null && opponents.Contains(t))
		  .ToList();

	   if (chosen.Count == 0 && opponents.Count > 0)
		  chosen.Add(opponents[0]); // retarget (use a random one if you prefer)

	   return chosen;
	}
	
	private void EndRound() {
		
		foreach (var dead in Enemies.Where(e => e.Dead).ToList()) {
		   Enemies.Remove(dead);
		   dead.QueueFree();
		}
		Queue = new TurnQueue(Team.Concat(Enemies).Where(c => !c.Dead).ToList());
	}

	private bool BattleOver() {
	   return Team.All(c => c.Dead) || Enemies.All(c => c.Dead);
	}
	
	// Hits every target in the list. Returns true if at least one of them was defeated.
	public void BasicAttack(Combatant attacker, List<Combatant> targets) {
	   foreach (var target in targets) {
		  // Skip anything invalid, including a target killed earlier in this same attack.
		  if (target == null || target.Dead)
			 continue;

		  CalculationResult result = DamageCalculator.basicAttack(attacker.Data.AttackDmg, target.Data.Defense);
		  GD.Print($"Combatant ({attacker.Data.Name}) attacks {target.Data.Name} for {result.Damage} Attack Points");
	
		  target.TakeDamage(result.Damage);

		  if (target.Dead) {
			 GD.Print($"{target.Data.Name} was defeated");
		  }
	   }
	}

	public List<Combatant> GetTeam() {
		return Team;
	}

	public List<Combatant> GetEnemies() {
		return Enemies;
	}
	public void SetLivingTeam() {
		Team = Team.Where(member => !member.Dead).ToList();
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
