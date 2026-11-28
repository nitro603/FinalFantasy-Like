using Godot;

public partial class AudioManager : Node
{
	[Export] public AudioStream TitleMusic { get; set; }
	[Export] public AudioStream BattleMusic { get; set; }
	[Export] public AudioStream HitSound { get; set; }
	[Export] public AudioStream StartSound { get; set; }
	[Export] public AudioStream DefendSound { get; set; }

	// Use "Music" and "SFX" after adding those buses in Godot's Audio panel.
	// "Master" works without any extra bus setup.
	[Export] public string MusicBus { get; set; } = "Master";
	[Export] public string SfxBus { get; set; } = "Master";

	private const int SfxPlayerCount = 8;
	private AudioStreamPlayer _musicPlayer;
	private AudioStreamPlayer[] _sfxPlayers;
	private int _nextSfxPlayer;

	public override void _Ready()
	{
		_musicPlayer = new AudioStreamPlayer
		{
			Name = "MusicPlayer",
			Bus = MusicBus
		};
		AddChild(_musicPlayer);

		_sfxPlayers = new AudioStreamPlayer[SfxPlayerCount];
		for (int i = 0; i < _sfxPlayers.Length; i++)
		{
			_sfxPlayers[i] = new AudioStreamPlayer
			{
				Name = $"SfxPlayer{i}",
				Bus = SfxBus
			};
			AddChild(_sfxPlayers[i]);
		}
	}

	public void PlayTitleMusic() => PlayMusic(TitleMusic);
	public void PlayBattleMusic() => PlayMusic(BattleMusic);

	public void PlayHit() => PlaySfx(HitSound);
	public void PlayStart() => PlaySfx(StartSound);
	public void PlayDefend() => PlaySfx(DefendSound);

	private void PlayMusic(AudioStream stream)
	{
		if (stream == null)
			return;

		if (_musicPlayer.Stream == stream && _musicPlayer.Playing)
			return;

		_musicPlayer.Stream = stream;
		_musicPlayer.Play();
	}

	private void PlaySfx(AudioStream stream)
	{
		if (stream == null || _sfxPlayers == null)
			return;

		AudioStreamPlayer player = _sfxPlayers[_nextSfxPlayer];
		_nextSfxPlayer = (_nextSfxPlayer + 1) % _sfxPlayers.Length;

		player.Stream = stream;
		player.Play();
	}
}
