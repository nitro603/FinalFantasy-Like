using Godot;
using System;

public partial class UnitStatusPanel : Node{
	
	[Export] public ProgressBar HPbar;

	public void SetValues(int currentHP, int maxHP) {
		HPbar.Value = currentHP;
		HPbar.MaxValue = maxHP;
	}

	public void ReduceByAttack(int damage) {
		double CurrentHp = HPbar.Value;
		HPbar.Value = CurrentHp - damage < 0 ? 0 : CurrentHp - damage;
	}
	
	
}
