using Godot;
using System.Collections.Generic;
using System.Linq;
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
			if (BattleOver())
				break;

			if (attacker.Dead)
				continue;

			var target = ResolveTarget(attacker);
			if (target == null)
				continue;

			BasicAttack(attacker, target);
		}

		EndRound();
	}
	
	private Combatant ResolveTarget(Combatant attacker) {
		var opponents = (attacker.Data.Enemy ? Team : Enemies)
			.Where(o => !o.Dead)
			.ToList();

		var chosen = opponents.FirstOrDefault(opponent => opponent.Data.Name == attacker.Decision.Target);
		return chosen ?? opponents.FirstOrDefault(); 
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
	
	public void BasicAttack(Combatant attacker, Combatant target) {
			CalculationResult result = DamageCalculator.basicAttack(attacker.Data.AttackDmg, target.Data.Defense);
			GD.Print($"Combatant ({attacker.Data.Name}) attacks {target.Data.Name} for {result.Damage} Attack Points");
	
			target.TakeDamage(result.Damage);
	
			if (target.Dead)
				GD.Print($"{target.Data.Name} was defeated");
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
