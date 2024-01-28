using Godot;
using System;

public partial class Shoebox : Area2D
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnCollision(Node node)
	{
		if(node.GetType().ToString() == "PlayerCharacter")
		{
			node.GetNode<PlayerCharacter>(node.GetPath()).giveShoe();
		}
		QueueFree();		
	}
}
