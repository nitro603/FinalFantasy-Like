using Godot;
using System;

public partial class GameFlow : Node
{
	private const string BattleScene = "res://main.tscn";

	public void StartGame() => ChangeScene(BattleScene);

	private void ChangeScene(string scenePath)
	{
		Error error = GetTree().ChangeSceneToFile(scenePath);
		if (error != Error.Ok)
		{
			GD.PushError($"Could not load scene '{scenePath}': {error}");
		}
	}
	public void RetryBattle()
	{
		Error error = GetTree().ReloadCurrentScene();

		if (error != Error.Ok)
			GD.PushError($"Could not reload the battle scene: {error}");
	}
}
