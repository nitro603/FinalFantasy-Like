using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public partial class GameController : Node {
	[Signal] public delegate void DecisionByPlayerEventHandler(int character, int action);
	
	[Export] public BattleManager BattleManager;
	[Export] public UIManager UIManager;
	[Export] public CommandMenu Menu;

	public List<Combatant> Team;
	public List<Combatant> LivingTeam;
	public List<Combatant> Enemies;
	public List<Combatant> Targets;
	public Combatant CurrentTeamMember;
	public Combatant Target;
	public bool targeting;
	public int CurrentPos;
	public int CurrentAction;
	
	
	// Called when the node enters the scene tree for the first time. 
	public override void _Ready() {
		Team = BattleManager.GetTeam();
		Enemies = BattleManager.GetEnemies();
		LivingTeam = Team;
		Targets = new List<Combatant>();
		CurrentPos = 0;

		StartTurn();
	}

	public void StartTurn() { 
		Menu.ActionChosen += action => {
			CurrentAction = action;
			CurrentTeamMember = LivingTeam.ElementAt(CurrentPos);

			if (action == 0) {
				UIManager.TargetSelectionMode();
				targeting = true;
			}
			
			GetLivingMembers();
			
			EmitSignal(SignalName.DecisionByPlayer, CurrentPos, action);
			
			if (!targeting) {
				CurrentPos++;
				if (CurrentPos == LivingTeam.Count) { 
					BattleManager.StartRound();
					CurrentPos = 0; 
				}
			}
			
		};
		UIManager.EnemiesSelected += targets => {
			GetLivingMembers();
			CurrentTeamMember.Decision = new BattleAction(CurrentAction, targets.ToList());
			CurrentPos++;
			if (CurrentPos == LivingTeam.Count) { 
				BattleManager.StartRound();
				CurrentPos = 0; 
			}
			Debug.Print(CurrentPos.ToString());
			targeting = false;
		};
	}

	public void GetLivingMembers() {
		LivingTeam = Team.Where(character => !character.Dead).ToList();
	}
}
