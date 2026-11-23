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
	public List<Combatant> Enemies;
	public Combatant CurrentAttacker;
	public Combatant Target;
	public int CurrentPos;
	public bool Targeting;
	
	
	// Called when the node enters the scene tree for the first time. 
	public override void _Ready() {
		Team = BattleManager.GetTeam();
		Enemies = BattleManager.GetEnemies();
		CurrentPos = 0;

		StartTurn();
		//set combatant decisions through command menu and target locks
		
		//we want this to communicate to UI manager to tell us who made the decision and what character it was
		//Flow
		//Game controller starts player turns --> sends signal to open command menu --> command menu takes
		//input of decision  --> sends decision back to UI manager which keeps track of which combatant's turn it was
		//--> UI manager sends signal to game controller containing combatant and decision
		//controller decides what to do from there:
		//If attack (execute battle manager logic with UI changes)
		//if ability --> send signal to minigame to spawn the specific character's minigame
		//if defense (execute battle manager logic)
		//if Nurture --> send signal to minigame to spawn baby minigame
	}

	public void StartTurn() { 
		Menu.ActionChosen += action => {
			Random rng = new Random();
			int index = rng.Next(0,Enemies.Count);
			
			CurrentAttacker = Team.ElementAt(CurrentPos);
			Target = Enemies.ElementAt(index);
			
			CurrentAttacker.Decision = new BattleAction(action, Target.Data.Name);
			EmitSignal(SignalName.DecisionByPlayer, CurrentPos, action);
			
			CurrentPos++;
			if (CurrentPos == 3) { 
				BattleManager.StartRound(); 
				CurrentPos = 0; 
			}
		};
	}

	
}
