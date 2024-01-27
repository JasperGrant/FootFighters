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
	private const float HOP_STRENGTH=600.0F;
	[Export] 
	private const float ONLY_Y_MOTION_SCALER=0.8F;
	[Export] 
	private const float ONLY_X_MOTION_SCALER=0.6F;




	private bool _isPlayer2 = false;
	private string _nodePath = "";

	private PackedScene FeatherScene = GD.Load<PackedScene>("res://scenes/feather.tscn");
	private PackedScene _laughScene = GD.Load<PackedScene>("res://scenes/laugh.tscn");

	//members
	private Variant _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity");
	private Vector2 _localVelocity = new(0,0);
    private Vector2 _inputControlVector = new(0,0);

	private float _baseScale =0.2F;

	private PlayerMappings _inputMappings = new();
	private AnimatedSprite2D _sprite2D;
	private CollisionShape2D _collisionShape2D;

	public Label _player_health_label;

	private int _health = 5;

	public void decrement_health(int diff){
		_health -= diff;
	}
    private int _allowedJumpAmount = 2;
    private int _currentJumps = 0;

	private Feather _featherRef;


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

		var _Feather = FeatherScene.Instantiate();
		AddChild(_Feather);
		
		_featherRef=GetNode<Feather>(_Feather.GetPath());
		_featherRef.Hello();
	}

	public override void _Process(double delta)
	{
		_player_health_label.Text = _health.ToString();
		if(_health < 1){
			//GetNode<Label>("Winner").Text = _isPlayer2 ? "Player 1 wins!" : "Player 2 wins!";
			GetTree().ChangeSceneToFile("res://scenes/laugh.tscn");
            var next_scene=_laughScene.Instantiate();
            GetNode<Node>("/root").AddChild(next_scene);
			GD.Print("Hello");
			GetParent().QueueFree();
		}
		// Called every frame. Delta is time since the last frame.
		// Update game logic here.
	}

	// public override void _Input(InputEvent @event)
	// {
	// 	//print for debugging
	// 	GD.Print(@event.AsText());
	// }


	public override void _PhysicsProcess(double delta)
	{
		//GD.Print(IsOnFloor().ToString());
		// Add the gravity.
		if (!IsOnFloor())
		{
			_localVelocity.Y += (float)_gravity *(float) delta;
			//_sprite2D.Play("jump");
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
			_localVelocity.X = direction * SPEED;

			var newSpriteScale = _sprite2D.Scale;
			var newColliderScale = _collisionShape2D.Scale;
			//allows shinking the character if stick is not fully pushed
			if(Input.IsActionJustPressed(_inputMappings.Special1))
			{
				newSpriteScale.X = direction*_baseSpriteScale;
				newColliderScale.X = direction*_baseColliderScale;
			}
			else
			{
			//allows shinking the character if stick is not fully pushed
				newSpriteScale.X = (direction>0 ? 1:-1)*_baseSpriteScale;
				newColliderScale.X = (direction>0 ? 1:-1)*_baseColliderScale;

			}

			_sprite2D.Scale = newSpriteScale;
			_collisionShape2D.Scale=newColliderScale;
			//_sprite2D.FlipH = direction<0;
			//GD.Print(Scale.X);
			if (IsOnFloor()){
				//_sprite2D.Play("run");
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
			if(IsAllowedToJump())
			{

                if (_inputControlVector.X==0)
                {
                    _localVelocity.X=0;
                    _localVelocity.Y = _inputControlVector.Y * HOP_STRENGTH * ONLY_Y_MOTION_SCALER;    
                }
                else if (_inputControlVector.Y==0)
                {
                    _localVelocity.X = _inputControlVector.X * HOP_STRENGTH * ONLY_X_MOTION_SCALER;
                    _localVelocity.Y = 0; 
                }
                else
                {
				_localVelocity.X = _inputControlVector.X * HOP_STRENGTH;
                _localVelocity.Y = _inputControlVector.Y * HOP_STRENGTH;
                }






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


			if (IsOnFloor()){
                // This handles the character slowing to a stop on the floor
                _localVelocity.X = Mathf.MoveToward(Velocity.X, 0, 5*SPEED*(float)delta);
				_sprite2D.Play("default");
			}

        }



		Velocity=_localVelocity;
		
		//GD.Print(Velocity.ToString());
	
		MoveAndSlide();
	}


    private bool IsAllowedToJump()
    {
        if (IsOnFloor() || IsOnWall())
        {
            _currentJumps=1;
            return true;
        }
        else if (_currentJumps<_allowedJumpAmount)
        {
            _currentJumps++;
            return true;
        }
        else
        {
            return false;
        }

    }











































































}






