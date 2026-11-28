using Godot;

public partial class TitleMenu : Control
{
	[Export] public Button PlayButton;
	

	public override void _Ready()
	{
		var flow = GetNode<GameFlow>("/root/GameFlow");

		PlayButton.Pressed += flow.StartGame;
	   
	}
}
