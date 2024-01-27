extends Button

const NEXT_SCENE_PATH: String = "scenes/arena_1.tscn"

func _on_pressed():
	print(get_tree_string())
	ResourceLoader.load_threaded_request(NEXT_SCENE_PATH)
	var next_scene = ResourceLoader.load_threaded_get(NEXT_SCENE_PATH)
	var next = next_scene.instantiate()
	get_node("/root").add_child(next)
	get_parent().queue_free()
