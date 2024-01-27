using System;
using System.Runtime.CompilerServices;
using Godot;




public readonly struct PlayerMappings
{
	public PlayerMappings(bool playerflag)
	{
		if (!playerflag)
		{
			Up="P1"+"Up";
			Down="P1"+"Down";
			Left="P1"+"Left";
			Right="P1"+"Right";
			Jump="P1"+"Jump";
			Special1="P1"+"Special1";
			Special2="P1"+"Special2";
			Special3="P1"+"Special3";
		}
		else
		{
			Up="P2"+"Up";
			Down="P2"+"Down";
			Left="P2"+"Left";
			Right="P2"+"Right";
			Jump="P2"+"Jump";
			Special1="P2"+"Special1";
			Special2="P2"+"Special2";
			Special3="P2"+"Special3";
		}
	}
	public readonly string Up {get; init;}
	public readonly string Down {get; init;}
	public readonly string Left {get; init;}
	public readonly string Right {get; init;}
	public readonly string Jump {get; init;}
	public readonly string Special1 {get; init;}
	public readonly string Special2 {get; init;}
	public readonly string Special3 {get; init;}
}



public partial class PlayerCharacter : CharacterBody2D
{
	//constants
	[Export] 
	private const float SPEED=300.0F;
	[Export] 
	private const float JUMP_VELOCITY=-400.0F;
	[Export] 
	private const float HOPSTRENGTH=400.0F;
	[Export] 
	private bool _isPlayer2 = false;
    private string _nodePath = "";


	//members
	private Variant _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity");
	private Vector2 _localVelocity = new(0,0);
    private Vector2 _inputControlVector = new(0,0);

    private float _baseScale =0.2F;

	private PlayerMappings _inputMappings = new();
	private AnimatedSprite2D _sprite2D;
	private CollisionShape2D _collisionShape2D;

	public Label _player_health_label;

	public int _health = 5;



	public override void _Ready()
	{
		// Called every time the node is added to the scene.
		// Initialization here.
		
		_sprite2D = GetNode<AnimatedSprite2D>("Sprite");
		_collisionShape2D = GetNode<CollisionShape2D>("Collision");
        _nodePath=GetNode<PlayerCharacter>(".").GetPath().ToString();

        if(_nodePath.IndexOf("Player2")!=-1)
        {
            _isPlayer2=true;
        }


        GD.Print(_nodePath);
		if (_isPlayer2)
		{
			_player_health_label = GetNode<Label>("/root/Arena 1/UI/Health_Bars/P2_Health");
		}
		else{
			_player_health_label = GetNode<Label>("/root/Arena 1/UI/Health_Bars/P1_Health");
		}
        _inputMappings=new PlayerMappings(_isPlayer2);

	}

	public override void _Process(double delta)
	{
		_player_health_label.Text = _health.ToString();
		// Called every frame. Delta is time since the last frame.
		// Update game logic here.
	}

	public override void _Input(InputEvent @event)
	{
		//print for debugging
		//GD.Print(@event.AsText());
	}


	public override void _PhysicsProcess(double delta)
	{
		//GD.Print(IsOnFloor().ToString());
		// Add the gravity.
		if (!IsOnFloor())
		{
			_localVelocity.Y += (float)_gravity *(float) delta;
			//_sprite2D.Play("jump");
		}
		// Handle jump.
		// if (Input.IsActionJustPressed(_inputMappings.Jump) && IsOnFloor())
		// {
		// 	_localVelocity.Y = JUMP_VELOCITY;
			
		// }
		// Handle mega jump
		if (Input.IsActionJustPressed(_inputMappings.Special2) && IsOnFloor())
		{
			_localVelocity.Y = JUMP_VELOCITY*2;
		}


        //get vector of input control direction
        var xdirection = Input.GetAxis(_inputMappings.Left, _inputMappings.Right);
        var ydirection = Input.GetAxis(_inputMappings.Up, _inputMappings.Down);
        _inputControlVector.X=xdirection;
        _inputControlVector.Y=ydirection;


        /*
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		var direction = Input.GetAxis(_inputMappings.Left, _inputMappings.Right);
		//GD.Print(direction.ToString());
		if (direction!=0F)
		{
			if(Input.IsActionJustPressed(_inputMappings.Special1))
			{
				_localVelocity.X = direction * SPEED;

				var newScale = _sprite2D.Scale;
				//allows shinking the character if stick is not fully pushed
				newScale.X = direction*_baseScale;
				_sprite2D.Scale = newScale;
				_collisionShape2D.Scale=newScale;
				//_sprite2D.FlipH = direction<0;
				//GD.Print(Scale.X);
				if (IsOnFloor()){
					//_sprite2D.Play("run");
				}

			} 
			else
			{
				_localVelocity.X = direction * SPEED;

				var newScale = _sprite2D.Scale;
				//this maps the controller iputs to match the full on off of the arrow keys
				newScale.X = (direction>0 ? 1:-1)*_baseScale;
				_sprite2D.Scale = newScale;
				_collisionShape2D.Scale=newScale;
				//_sprite2D.FlipH = direction<0;
				//GD.Print(Scale.X);
				if (IsOnFloor()){
					//_sprite2D.Play("run");
				}



			}

		}
		else
		{
			// This handles the character slowing to a stop
			_localVelocity.X = Mathf.MoveToward(Velocity.X, 0, 5*SPEED*(float)delta);

			if (IsOnFloor()){
				_sprite2D.Play("default");
			}
		}

        */
        if (Input.IsActionJustPressed(_inputMappings.Jump) && _inputControlVector.Length()!=0)
        {
			if(IsOnFloor())
			{
				_localVelocity.X = _inputControlVector.X * HOPSTRENGTH;
                _localVelocity.Y = _inputControlVector.Y * HOPSTRENGTH;
                GD.Print($"Hop X: {_localVelocity.X}, Hop Y: {_localVelocity.Y}");

				// var newScale = _sprite2D.Scale;
				// //allows shinking the character if stick is not fully pushed
				// newScale.X = xdirection*_baseScale;
				// _sprite2D.Scale = newScale;
				// _collisionShape2D.Scale=newScale;
				//_sprite2D.FlipH = direction<0;
				//GD.Print(Scale.X);
				if (IsOnFloor()){
					//_sprite2D.Play("run");
				}

			} 


        }
        else
        {
			// This handles the character slowing to a stop
			_localVelocity.X = Mathf.MoveToward(Velocity.X, 0, 5*SPEED*(float)delta);

			if (IsOnFloor()){
				_sprite2D.Play("default");
			}

        }



		Velocity=_localVelocity;
		
		//GD.Print(Velocity.ToString());
	
		MoveAndSlide();
	}
}






