extends NinePatchRect

@export var menu: Node
signal Sucess
signal Failure

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	menu.ActionChosen.connect(_spawnMinigame)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func _spawnMinigame(character: int):
	print(character)
	#add_child()

func MiniGame_Win():
	emit_signal("Sucess")
	
func Minigame_loss():
	emit_signal("Failure")
