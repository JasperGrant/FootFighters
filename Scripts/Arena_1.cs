using Godot;
using System;


public partial class Arena_1 : Node
{

	private TextureRect BG1;
	private TextureRect BG2;
	private TextureRect BG3;
	private TextureRect BG4;
	private TextureRect BG5;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BG1 = GetNode<TextureRect>("RedBG");
		BG2 = GetNode<TextureRect>("BlueBG");
		BG3 = GetNode<TextureRect>("GreenBG");
		BG4 = GetNode<TextureRect>("YellowBG");
		BG5 = GetNode<TextureRect>("PurpleBG");

		switch(1+ (GD.Randi() % 5))
		{
			case 1 :
				BG1.Visible = true;
				break;
			case 2 :
				BG2.Visible = true;
				break;
			case 3 :
				BG3.Visible = true;
				break;
			case 4 :
				BG4.Visible = true;
				break;
			case 5 :
				BG5.Visible = true;
				break;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
