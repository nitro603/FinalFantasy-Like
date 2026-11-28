using Godot;

public partial class GameOver_Overlay : Control
{
	[Export] public BattleManager BattleManager;
	[Export] public Control BattleUI;
	[Export] public Label MessageLabel;
	[Export] public Button RetryButton;

	public override void _Ready()
	{
		Hide();

		BattleManager.BattleEnded += OnBattleEnded;
		RetryButton.Pressed += OnRetryPressed;
	}

	private void OnBattleEnded(bool teamDefeated)
	{
		MessageLabel.Text = teamDefeated ? "GAME OVER" : "VICTORY";
		RetryButton.Text = teamDefeated ? "Retry" : "Play Again";

		BattleUI.Hide();
		Show();
	}

	private void OnRetryPressed()
	{
		GetNode<GameFlow>("/root/GameFlow").RetryBattle();
	}
}
