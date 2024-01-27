using System;
using Godot;




public readonly struct Player1Mappings
{
    public Player1Mappings()
    {
        Jump="P1"+"Jump";
        Left="P1"+"Left";
        Right="P1"+"Right";
        MegaJump="P1"+"MegaJump";
        Special1="P1"+"Special1";
        Special2="P1"+"Special2";
    }

    readonly string Jump {get; init;}
    readonly string Left {get; init;}
    readonly string Right {get; init;}
    readonly string MegaJump {get; init;}
    readonly string Special1 {get; init;}
    readonly string Special2 {get; init;}


}

public readonly struct Player2Mappings
{
    public Player2Mappings()
    {
        Jump="P2"+"Jump";
        Left="P2"+"Left";
        Right="P2"+"Right";
        MegaJump="P2"+"MegaJump";
        Special1="P2"+"Special1";
        Special2="P2"+"Special2";


    }

    readonly string Jump {get; init;}
    readonly string Left {get; init;}
    readonly string Right {get; init;}
    readonly string MegaJump {get; init;}
    readonly string Special1 {get; init;}
    readonly string Special2 {get; init;}


}



public partial class PlayerCharacter : CharacterBody2D
{
    //constants
    [Export]
    private const float SPEED=300.0F;
    [Export]
    private const float JUMP_VELOCITY=-400.0F;
      [Export]
    private const bool PLAYER2=false;

    //members
    private Variant _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity");
    private Vector2 _localVelocity = new(0,0);

    public AnimatedSprite2D _sprite2D;
    private CollisionShape2D _collisionShape2D;



    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here.
        GD.Print("Hello from C# to Godot :)");
        _sprite2D = GetNode<AnimatedSprite2D>("Sprite2D");
        _collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");

        if (PLAYER2)
        {
        }

    }

    public override void _Process(double delta)
    {
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
            _sprite2D.Play("jump");
        }
        // Handle jump.
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            _localVelocity.Y = JUMP_VELOCITY;
            
        }
        // Handle mega jump
        if (Input.IsActionJustPressed("megajump") && IsOnFloor())
        {
            _localVelocity.Y = JUMP_VELOCITY*2;
        }
        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        var direction = Input.GetAxis("left", "right");
        //GD.Print(direction.ToString());
        if (direction!=0F)
        {
            if(Input.IsActionJustPressed("special_action_1"))
            {
                _localVelocity.X = direction * SPEED;

                var newScale = _sprite2D.Scale;
                newScale.X = direction*2;
                _sprite2D.Scale = newScale;
                _collisionShape2D.Scale=newScale;
                //_sprite2D.FlipH = direction<0;
                //GD.Print(Scale.X);
                if (IsOnFloor()){
                    _sprite2D.Play("run");
                }

            } 
            else
            {




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






