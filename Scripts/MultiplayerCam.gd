extends Camera2D

@export var move_speed = 30
@export var zoom_speed = 3.0
@export var min_zoom = 5
@export var max_zoom = 0.5
@export var margin = Vector2(400, 200)

var targets = []

@onready var screen_size = get_viewport_rect().size

func add_target(t):
	if not t in targets:
		targets.append(t)
		
func remove_target(t):
	if t in targets:
		targets.remove(t)
		

func _process(delta):
	if !targets:
		return
		
	var p = Vector2.ZERO
	for target in targets:
		p += target.position
	p /= targets.size()
	position = lerp(position, p, move_speed * delta)

	# Find the zoom that will contain all targets
	var r = Rect2(position, Vector2.ONE)
	for target in targets:
		r = r.expand(target.position)
	r = r.grow_individual(margin.x, margin.y, margin.x, margin.y)
	var z
	if r.size.x > r.size.y * screen_size.aspect():
		z = 1 / clamp(r.size.x / screen_size.x, max_zoom, min_zoom)
	else:
		z = 1 / clamp(r.size.y / screen_size.y, max_zoom, min_zoom)
	zoom = lerp(zoom, Vector2.ONE * z, zoom_speed * delta)
	
		
