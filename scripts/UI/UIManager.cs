using Godot;
using System;
using System.Diagnostics;
using System.Linq;

public partial class UIManager : Node {

	private BattleManager BattleManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		
		BattleManager = GetChildren().OfType<BattleManager>().FirstOrDefault();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
