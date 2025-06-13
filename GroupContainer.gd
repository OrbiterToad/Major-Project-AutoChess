@tool
extends MarginContainer

func _ready() -> void:
	RenderingServer.canvas_item_set_canvas_group_mode(get_canvas_item(), RenderingServer.CANVAS_GROUP_MODE_TRANSPARENT, 0.0, true, 0.0, false)
