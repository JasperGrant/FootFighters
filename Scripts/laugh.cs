using Godot;
using System;

public partial class laugh : Node
{
	// Called when the node enters the scene tree for the first time.


	private TextureRect _blue;
	private TextureRect _red;
	private TextureRect _redFoot;
	private TextureRect _blueFoot;


	public void gameOver(bool blueLost)
	{
		if(blueLost)
		{
			_blue.Visible = false;
			_blueFoot.Visible = false;
			_red.Visible = true;
			_redFoot.Visible = true;
		}
		else
		{
			_blue.Visible = true;
			_blueFoot.Visible = true;
			_red.Visible = false;
			_redFoot.Visible = false;
		}
	}
	public override void _Ready()
	{
		_blue = GetNode<TextureRect>("Blue");
		_red = GetNode<TextureRect>("Red");
		_redFoot = GetNode<TextureRect>("RedToenails");
		_redFoot = GetNode<TextureRect>("RedToenails");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
