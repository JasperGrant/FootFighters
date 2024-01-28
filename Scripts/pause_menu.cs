using Godot;

public partial class pause_menu : Control
{
	private Button _resumeButton;
	private GameManager _gameManager;
	private void _on_ready()
{
	Visible = false;
	_gameManager = GetNode<GameManager>("/root/BaseNode/GameManager");
}

public bool is_paused = false;

public void toggle_paused(){
	is_paused = !is_paused;
	Visible = is_paused;
	if (is_paused)
	{
		_resumeButton = GetNode<Button>("./VBoxContainer/ResumeButton");
		_resumeButton.GrabFocus();
		//_resumeButton.CallDeferred("GrabFocus");
	}
	else
	{
		_gameManager.UnpausedArena1();
	}


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
