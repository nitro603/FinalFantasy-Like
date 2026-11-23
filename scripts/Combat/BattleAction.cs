using Godot;
using System;


public class BattleAction {
	
	public int Decision;
	public string Target;

	public BattleAction(int action, string target) {
		Decision = action;
		
		if (action == 0 || action == 1) {
			Target = target; 
		}
	}
	
}
