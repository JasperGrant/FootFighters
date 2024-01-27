extends TileMap

@onready var multi_cam = %MultiCam
@onready var player_1 = $"../Player1"
@onready var player_2 = $"../Player2"

# Called when the node enters the scene tree for the first time.
func _ready():
	multi_cam.add_target(player_1)
	multi_cam.add_target(player_2)
	
