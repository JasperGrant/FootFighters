using Godot;
using System;


public partial class Shoebox : Area2D
{
	bool visible = false;
	private Timer _timer;
	private AnimatedSprite2D _sprite;
	private CollisionShape2D _collider;
	private AudioStreamPlayer _sound;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_sprite = GetNode<AnimatedSprite2D>("Sprite");
		_collider= GetNode<CollisionShape2D>("Collider");
		_sound = GetNode<AudioStreamPlayer>("Sound");
		_sprite.Visible = false;
		SetCollisionLayerValue(1,false);
		SetCollisionMaskValue(1,false);
		SetCollisionLayerValue(2,false);
		SetCollisionMaskValue(2,false);
		SetCollisionLayerValue(3,false);
		SetCollisionMaskValue(3,false);
		SetCollisionLayerValue(4,false);
		SetCollisionMaskValue(4,false);	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void OnCollision(Node node)
	{
		if(node.GetType().ToString() == "PlayerCharacter")
		{
			node.GetNode<PlayerCharacter>(node.GetPath()).PowerUp();
		}
		_sound.Play();
		visible = false;
		_sprite.Visible = false;
		SetCollisionLayerValue(1,false);
		SetCollisionMaskValue(1,false);
		SetCollisionLayerValue(2,false);
		SetCollisionMaskValue(2,false);
		SetCollisionLayerValue(3,false);
		SetCollisionMaskValue(3,false);
		SetCollisionLayerValue(4,false);
		SetCollisionMaskValue(4,false);	
		}

	public void TimeOut()
	{
		if ((!visible) && (GD.Randi() % 5 == 0))
		{
			visible = true;
			_sprite.Visible = true;
			SetCollisionLayerValue(1,true);
			SetCollisionMaskValue(1,true);
			SetCollisionLayerValue(2,true);
			SetCollisionMaskValue(2,true);
			SetCollisionLayerValue(3,true);
			SetCollisionMaskValue(3,true);
			SetCollisionLayerValue(4,true);
			SetCollisionMaskValue(4,true);
		}
	}
	
}
