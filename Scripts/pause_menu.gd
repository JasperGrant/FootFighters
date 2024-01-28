extends Control

var is_paused = false

func set_is_paused(value):
	is_paused = value
	get_tree().paused = value
	visible = value


func _on_resume_button_pressed():
	# Set unpaused
	pass
	

func _on_main_menu_button_pressed():
	var next_scene = preload("res://scenes/main_menu.tscn").instantiate()
	get_node("/root/BaseNode").add_child(next_scene)
	get_parent().queue_free()
	

func _on_quit_button_pressed():
	get_tree().quit()
