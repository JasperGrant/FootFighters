extends Button

const NEXT_SCENE_PATH: String = "res://scenes/arena_1.tscn"

func _on_pressed():
	print(get_tree_string())
	#ResourceLoader.load_threaded_request(NEXT_SCENE_PATH)
	#var next_scene = ResourceLoader.load_threaded_get(NEXT_SCENE_PATH)
	var next_scene = preload(NEXT_SCENE_PATH).instantiate()
	get_node("/root/BaseNode").add_child(next_scene)
	get_parent().queue_free()
