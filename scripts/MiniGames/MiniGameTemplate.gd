extends NinePatchRect
signal win
signal loss
#minigames should be 160x90, position at 30 x 30


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	win.emit()


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
func minigame_end():
	hide()
	
