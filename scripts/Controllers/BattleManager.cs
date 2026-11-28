using Godot;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public partial class BattleManager : Node {
	[Signal] public delegate void BattleEndedEventHandler(bool teamDefeated);
	
	private TurnQueue Queue;
	private List<Combatant> Team;
	private List<Combatant> Enemies;
	private bool _battleEnded;

	public override void _Ready() {
		var combatants = FindChildren("*", "Node", true, false).OfType<Combatant>().ToList();

		Team = new List<Combatant>();
		Enemies = new List<Combatant>();
		GroupCombatants(combatants);
		GD.Print(Team.ElementAt(0).Data.Name);
		SetOpponents();

		Queue = new TurnQueue(combatants);
	}
	
	public void StartBattle() {
		if (FinishBattleIfOver())
			return;

		MakeEnemyDecisions();

		foreach (var character in Queue.CurrentTurnOrder.ToList())
		{
			if (FinishBattleIfOver())
				return;

			if (character.Decision.Action != 0 || character.Dead)
				continue;

			var targets = ResolveTargets(character);
			if (targets.Count == 0)
				continue;

			BasicAttack(character, targets);

			// A final attack can end the battle immediately.
			if (FinishBattleIfOver())
				return;
		}

		EndRound();
	}
	
	private bool FinishBattleIfOver()
	{
		if (_battleEnded)
			return true;

		bool teamDefeated = Team.Count == 0 || Team.All(member => member.Dead);
		bool enemiesDefeated = Enemies.Count == 0 || Enemies.All(enemy => enemy.Dead);

		if (!teamDefeated && !enemiesDefeated)
			return false;

		_battleEnded = true;
		EmitSignal(SignalName.BattleEnded, teamDefeated);
		return true;
	}
	

	private List<Combatant> ResolveTargets(Combatant attacker) {
	   var picked = attacker.Decision.Targets ?? new List<Combatant>();
	   var opponents = (attacker.Data.Enemy ? Team : Enemies).Where(o => !o.Dead).ToList();

	   var chosen = picked
		  .Where(t => t != null && opponents.Contains(t))
		  .ToList();

	   if (chosen.Count == 0 && opponents.Count > 0)
		  chosen.Add(opponents[0]); 

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
