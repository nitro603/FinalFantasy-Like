using Godot;
using System;
using System.Diagnostics;

public partial class Combatant : Node {
	[Export] public int MaxHp = 100;
	[Export] public int CurrentHp = 100; 
	[Export] public CombatantStats Data;
	[Export] public UnitStatusPanel Panel;
	private Sprite2D Sprite;
	private AutoBattler _CombatantAI;
	public BattleAction Decision;
	public bool Highlighted;
	public bool Dead;


	public override void _Ready() {
		Sprite = GetNode<Sprite2D>("VBoxContainer/Control/Character/Texture");
		Sprite.SetTexture(Data.Sprite);
		Panel.SetValues(CurrentHp, MaxHp);
		
	}

	public void TakeDamage(int opponentDamage) {
		Panel.ReduceByAttack(opponentDamage);
		CurrentHp = CurrentHp - opponentDamage < 0 ? 0 : CurrentHp - opponentDamage;
		Dead = CurrentHp == 0;
	}

	public void SetAI(AutoBattler ai) {
		_CombatantAI = ai;
	}

	public void GenerateDecision() {
		Decision = _CombatantAI.Choose();
	}

	public void SetHighlighted(bool highlighted) {
		Sprite.Modulate = highlighted ? Colors.Red : Colors.White;
	}

}
