extends Node
@export var game_controler: Node
@export var reina_minigame: Node
@export var whiskers_minigame: Node
@export var pedrolino_minigame: Node
@export var baby_minigame: Node
signal Sucess
signal Failure

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	game_controler.DecisionByPlayer.connect(spawn_mini_game)
	reina_minigame.win.connect(mini_game_win)
	pedrolino_minigame.win.connect(mini_game_win)
	whiskers_minigame.win.connect(mini_game_win)
	baby_minigame.win.connect(mini_game_win)
	reina_minigame.loss.connect(mini_game_loss)
	pedrolino_minigame.loss.connect(mini_game_loss)
	whiskers_minigame.loss.connect(mini_game_loss)
	baby_minigame.loss.connect(mini_game_loss)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func spawn_mini_game(character: int, action: int):
	if action == 1:
		if character == 0:
			$PedrolinoMiniGame.show()
		if character == 1:
			$ReinaMiniGame.show()
		if character == 2:
			$WhiskersMiniGame.show()
	if action == 3:
		$BabyMiniGame.show()

func mini_game_win():
	print("fuck you")
	emit_signal("Sucess")
	
func mini_game_loss():
	emit_signal("Failure")
