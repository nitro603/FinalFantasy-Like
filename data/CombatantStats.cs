using System;
using Godot;

[GlobalClass]
public partial class CombatantStats : Resource {
	[Export] public string Name { get; set; } = "Combatant_Name";
	[Export] public Texture2D Sprite { get; set; } 
 	[Export] public int AttackDmg { get; set; } = 10;
	[Export] public int Defense { get; set; } = 5;
	[Export] public int Agility { get; set; } = 25;
	[Export] public int TargetRange { get; set; } = 1;
	[Export] public bool Enemy;


}
