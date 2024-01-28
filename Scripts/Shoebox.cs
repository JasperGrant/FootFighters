using Godot;
using System;


public partial class Shoebox : Area2D
{
	bool visible = false;
	private Timer _timer;
	private AnimatedSprite2D _sprite;
	private CollisionObject2D _collider;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_sprite = GetNode<AnimatedSprite2D>("Sprite");
		_collider= GetNode<CollisionObject2D>("Collider");
		_sprite.Visible = false;
		SetCollisionLayerValue(4,false);
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
			visible = false;
			_sprite.Visible = false;
			SetCollisionLayerValue(4,false);		
	}

	public void TimeOut()
	{
		if ((!visible) && (GD.Randi() % 5 == 0))
		{
			visible = true;
			_sprite.Visible = true;
			SetCollisionLayerValue(4,true);
		}
	}
	
}
