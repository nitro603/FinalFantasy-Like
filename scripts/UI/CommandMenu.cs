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

	private bool _locked;

	public override void _Ready()
	{
		
		AttackButton.Pressed += () => OnActionPressed(CombatAction.Attack);
		AbilityButton.Pressed += () => OnActionPressed(CombatAction.Ability); 
		DefendButton.Pressed += () => OnActionPressed(CombatAction.Defend);
		NurtureButton.Pressed += () => OnActionPressed(CombatAction.Nurture);
		//Debug.Print("hello");
		//Lock();
	}
	
	
	public void Open()
	{
		_locked = false;           
		Show();                    
		AttackButton.GrabFocus(); 
	}
	
	public void Lock()
	{
		_locked = true; 
		Hide();         
	}
	
	private void OnActionPressed(CombatAction action)
	{
		if (_locked) {
			return;
		}  
		//Lock();
		
		EmitSignal(SignalName.ActionChosen, (int)action);
	}
}
