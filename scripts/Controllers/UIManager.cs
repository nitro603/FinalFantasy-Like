using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class UIManager : Control {
	[Signal] public delegate void EnemiesSelectedEventHandler(Array<Combatant> targets);
	
	[Export] public BattleManager BattleManager;
	[Export] public CommandMenu Menu;
	[Export] public GameController Controller;
	
	private List<Combatant> Team;
	private List<Combatant> Enemies;
	private Combatant CurrentMember;
	private Queue<Combatant> Targets;
	private int TargetRange;
	private int CurrentRange;
	private bool SelectingTarget;
	
	// Called when the node enters the scene 5tree for the first time.
	public override void _Ready() {
		Team = BattleManager.GetTeam();
		Enemies = BattleManager.GetEnemies();
		Targets = new Queue<Combatant>();
		CurrentRange = 0;
		
		Controller.DecisionByPlayer += (character, action) => {
			BattleManager.SetLivingTeam();
			Team = BattleManager.GetTeam();
			CurrentMember = Team.ElementAt(character);
			TargetRange = CurrentMember.Data.TargetRange;
			
			string choice = action == 0 ? "Attack" : action == 1 ? "Ability" : action == 2 ? "Defend" : "Nurture";
			Debug.Print(CurrentMember.Data.Name + " has chosen to cast " + choice);
		};
		
		ConnectToEnemies();
		Menu.ConfirmButton.Pressed += () => OnConfirmButtonPressed();
		Menu.BackButton.Pressed += () => OnBackButtonPressed();
	}

	
	public void ConnectToEnemies() {
		for (int pos = 0; pos < Enemies.Count; pos++) {
			Combatant enemy = Enemies[pos];
			
			enemy.Panel.EnemySelect.Pressed += () => {
				Targets.Enqueue(enemy);
				CurrentRange += 1;
				
				if (CurrentRange > TargetRange) {
					Combatant priorEnemy = Targets.Dequeue();
					
					priorEnemy.SetHighlighted(false);
					CurrentRange -= 1;
				}
				HighlightEnemies();
			};
		}
	}
	
	public void OnConfirmButtonPressed() {
		if (Targets.Count > 0) {
			Array<Combatant> targets = new Array<Combatant>(Targets);
			CurrentRange = 0;
			
			EmitSignal(SignalName.EnemiesSelected, targets);
			HideEnemies();
			ClearEnemiesSelected();
			Menu.Open();
		}
	}
	public void OnBackButtonPressed() {
		Menu.Open();
		HideEnemies();
	}
	
	public void TargetSelectionMode() {
		Menu.Lock();
		ShowEnemies();
	}
	

	public void ShowEnemies() {
		foreach (var member in Enemies) {
			member.Panel.EnemySelect.Show();
		}
	}
	public void HideEnemies() {
		foreach (var member in Enemies) {
			member.Panel.EnemySelect.Hide();
		}
	}

	public void ClearEnemiesSelected() {
		foreach (var member in Enemies) {
			member.SetHighlighted(false);
		}
		Targets = new Queue<Combatant>();
	}

	public void HighlightEnemies() {
		foreach (var enemy in Targets) {
			enemy.SetHighlighted(true);
		}
	}

}
