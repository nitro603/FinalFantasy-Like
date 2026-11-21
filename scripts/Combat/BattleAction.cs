using Godot;
using System;


public class BattleAction {
	
	public int Decision;
	public string Target;
	public string Effect;

	public BattleAction(int action, string target, string effect) {
		Decision = action;
		
		if (action == 0 || action == 1) {
			Target = target; 
			Effect = effect;
		}
	}
	
}
