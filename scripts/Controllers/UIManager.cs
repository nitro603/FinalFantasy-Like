using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;
using System.Threading.Tasks;

public partial class UIManager : Control {
	//[Signal] public delegate void DecisionByPlayer(int character, int action);
	
	[Export] public BattleManager BattleManager;
	[Export] public CommandMenu Menu;
	[Export] public GameController Controller;
	private List<Combatant> Team;
	private List<Combatant> Enemies;
	private int CurrentPos;
	private Combatant CurrentMember;
	private Combatant CurrentTarget;
	private bool SelectingTarget;
	
	// Called when the node enters the scene 5tree for the first time.
	public override void _Ready() {
		
		
		
		Team = BattleManager.GetTeam();
		Enemies = BattleManager.GetEnemies();
		CurrentPos = 0;
		//foreach (var Member in Team) {
			//Debug.Print(Member.Data.Name);
		Controller.DecisionByPlayer += (character, action) => {
			string choice = action == 0 ? "Attack" : action == 1 ? "Ability" : action == 2 ? "Defend" : "Nurture";
			Debug.Print(Team.ElementAt(character).Data.Name + " has chosen to cast " + choice);
		};
			//}
			//start turn
			//while match is not over
			//while turn not done

	}
	
	
	/*public async Task startTurn() {
		await ToSignal(Menu, CommandMenu.SignalName.ActionChosen);
		Menu.ActionChosen += (action) => {
			
			Combatant Current = Team.ElementAt(CurrentPos);
			Debug.Print(action + Current.Data.Name);
			Current.Decision = new BattleAction(action, "MonkeButBigger", "none");
			
			CurrentPos += 1;
			if (CurrentPos == 3) {
				CurrentPos = 0;
				BattleManager.StartRound();
			}
		};
	}*/
}
