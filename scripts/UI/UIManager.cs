using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;
using System.Threading.Tasks;

public partial class UIManager : Control {

	[Export]public BattleManager BattleManager;
	[Export] public CommandMenu Menu;
	private List<Combatant> Team;
	private int CurrentPos;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		CurrentPos = 0;
		Team = BattleManager.GetTeam();
		//foreach (var Member in Team) {
			//Debug.Print(Member.Data.Name);
		startTurn();
		//}
		//start turn
		//while match is not over
		//while turn not done
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public async Task startTurn() {
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
	}
}
