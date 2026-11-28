using Godot;

public partial class TitleMenu : Control
{
	[Export] public Button PlayButton;

	public override void _Ready()
	{
		var flow = GetNode<GameFlow>("/root/GameFlow");
		var audio = GetNode<AudioManager>("/root/AudioManager");

		audio.PlayTitleMusic();

		PlayButton.Pressed += () =>
		{
			//audio.PlayStart();
			flow.StartGame();
		};
	   
	}
}
