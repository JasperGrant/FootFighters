using Godot;
using System;
using System.Numerics;

public partial class PowerIcon : AnimatedSprite2D
{

	bool isP1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	
	}

	public void powerSignal(String power)
	{
		Play(power);
		if(power == "Shrink")
		{
			Scale = new Godot.Vector2(0.3F,0.3F);
		}
		else
		{
			Scale = new Godot.Vector2(0.1F,0.1F);
		}
	}
}
