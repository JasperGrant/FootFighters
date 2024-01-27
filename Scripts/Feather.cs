using System;
using Godot;

public partial class Feather : RigidBody2D
{

	public int X = 5;
	public Godot.Vector2 velocity = new(-300,300);
	private AnimatedSprite2D sprite;

	private string sender;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//ApplyImpulse(velocity);
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
		if(node.GetType().ToString() == "PlayerCharacter")
		{
			node.GetNode<PlayerCharacter>(node.GetPath()).decrement_health(1);
		}
		QueueFree();		
	}

	public void setVelo(float X, float Y)
	{
		velocity = new(X,Y);
		ApplyImpulse(velocity);
	}



}
