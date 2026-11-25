using System;
using System.Diagnostics;
using Godot;

enum CombatAction {
	Attack,
	Ability,
	Defend,
	Nurture
}


public partial class CommandMenu : Control
{
	[Signal] public delegate void ActionChosenEventHandler(int action);
	
	[Export] public Button AttackButton;  
	[Export] public Button AbilityButton; 
	[Export] public Button DefendButton; 
	[Export] public Button NurtureButton;
	[Export] public Button ConfirmButton;
	[Export] public Button BackButton;

	private bool _locked;

	public override void _Ready()
	{
		
		AttackButton.Pressed += () => OnActionPressed(CombatAction.Attack);
		AbilityButton.Pressed += () => OnActionPressed(CombatAction.Ability); 
		DefendButton.Pressed += () => OnActionPressed(CombatAction.Defend);
		NurtureButton.Pressed += () => OnActionPressed(CombatAction.Nurture);
		
		BackButton.Pressed += () => Open();
		//Debug.Print("hello");
		//Lock();
	}
	
	
	public void Open() {
		ConfirmButton.Hide();
		BackButton.Hide();
		AttackButton.Show();  
		AbilityButton.Show(); 
		DefendButton.Show(); 
		NurtureButton.Show();	
	}
	
	public void Lock()
	{
		AttackButton.Hide();  
		AbilityButton.Hide(); 
		DefendButton.Hide(); 
		NurtureButton.Hide();
		ConfirmButton.Show();
		BackButton.Show();
	}
	
	private void OnActionPressed(CombatAction action) {
		EmitSignal(SignalName.ActionChosen, (int)action);
	}
}
