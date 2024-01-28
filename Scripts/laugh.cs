using Godot;
using System;

public partial class laugh : Node
{
	// Called when the node enters the scene tree for the first time.


	private TextureRect _blue;
	private TextureRect _red;

	public void gameOver(bool blueLost)
	{
		if(blueLost)
		{
			_blue.Visible = false;
			_red.Visible = true;
		}
		else
		{
			_blue.Visible = true;
			_red.Visible = false;
		}
	}
	public override void _Ready()
	{
		_blue = GetNode<TextureRect>("Blue");
		_red = GetNode<TextureRect>("Red");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
