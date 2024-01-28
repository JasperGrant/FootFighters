using Godot;
using System;

public partial class laugh : Node
{
	// Called when the node enters the scene tree for the first time.


	private TextureRect _blue;
	private TextureRect _red;
	private TextureRect _redFoot;
	private TextureRect _blueFoot;

	private Button _playAgainButton;

	private Timer _playAgainTimer;

	public void gameOver(bool blueLost)
	{
		GD.Print("In Game Over");
		if(blueLost)
		{
			_blue.Visible = false;
			_blueFoot.Visible = true;
			_red.Visible = true;
			_redFoot.Visible = false;
		}
		else
		{
			_blue.Visible = true;
			_blueFoot.Visible = false;
			_red.Visible = false;
			_redFoot.Visible = true;
		}
		GD.Print("Game Over Done");
	}
	public override void _Ready()
	{
		_blue = GetNode<TextureRect>("Blue");
		_red = GetNode<TextureRect>("Red");
		_blueFoot = GetNode<TextureRect>("BlueToenails");
		_redFoot = GetNode<TextureRect>("RedToenails");
		_playAgainTimer = new Timer();
		AddChild(_playAgainTimer);
		_playAgainTimer.WaitTime=2;
		_playAgainTimer.Timeout +=GrabResume;
		_playAgainTimer.Start();
		GD.Print("Ready done");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void GrabResume()
	{
		_playAgainButton = GetNode<Button>("FinishButton");
		_playAgainButton.GrabFocus();
	}


}
