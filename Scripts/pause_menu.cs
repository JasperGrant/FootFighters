using Godot;

public partial class pause_menu : Control
{
	
	private void _on_ready()
{
	Visible = false;
}

public bool is_paused = false;

public void toggle_paused(){
	is_paused = !is_paused;
	Visible = is_paused;
}

private void _on_resume_button_pressed()
{
	toggle_paused();
}


private void _on_quit_button_pressed()
{
	GetTree().Quit();
}


}
