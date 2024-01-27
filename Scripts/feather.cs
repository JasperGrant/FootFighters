using Godot;
using System;
using System.Numerics;
using System.Runtime.Intrinsics;

public partial class Feather : RigidBody2D
{

	public int X = 5;
	public Godot.Vector2 velocity = new(-300,300);
	private AnimatedSprite2D sprite;

	public void Hello()
	{
		GD.Print("Hello Bob");
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ApplyImpulse(velocity);
		GravityScale = 0;
		ContactMonitor = true;
		MaxContactsReported = 1000;
		GlobalRotation = velocity.Angle();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	public void OnCollision(Node node)
	{
		//QueueFree();
	}
}
