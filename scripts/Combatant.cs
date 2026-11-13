using Godot;
using System;
using System.Diagnostics;
using Final_Fantasy_Like.scripts;

public partial class Combatant : Node
{
	[Export] private int _maxHp = 100;
	[Export] public int CurrentHp = 100; 
	[Export] public CombatantStats Data;
	private AutoBattler _CombatantAI;
	public BattleAction Decision;
	

	public void TakeDamage(int opponentDamage)
	{
		CurrentHp = CurrentHp - opponentDamage < 0 ? 0 : CurrentHp - opponentDamage;
	}

	public void SetAI(AutoBattler ai)
	{
		_CombatantAI = ai;
	}

	public void GenerateDecision()
	{
		Decision = _CombatantAI.Choose();
	}
	
	
}
