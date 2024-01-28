using Godot;

public partial class GameManager : Node
{
	private bool _debugPrintCycleFlag = false;
	private string _pauseKey = "Pause";

	public enum EGameState{
		Startup,
		MainMenu,
		InArenaSetup,
		InArenaPlay,
		InArenaPause,
		WinScreen
	}

	public EGameState CurrentGameState = EGameState.Startup;

	private int _currentArena=0;

	//These references are only valid while in arena
	private PlayerCharacter _player1Reference;
	private PlayerCharacter _player2Reference;
	
	private pause_menu pause;

	private AudioStreamPlayer _introMusic;
	private AudioStreamPlayer _loopMusic;

	public int WinnerNumber = 0;

	private Timer _countdownTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CurrentGameState=EGameState.Startup;
		_introMusic = GetNode<AudioStreamPlayer>("IntroMusic");
		_loopMusic = GetNode<AudioStreamPlayer>("LoopMusic");
		GD.Print("GM is online");
		pause = GetNode<pause_menu>("/root/BaseNode/Arena 1/Pause Menu");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if (_debugPrintCycleFlag)
		// {
		// 	GD.Print("GM is alive 1");
		// 	_debugPrintCycleFlag=false;
		// }
		// else
		// {
		// 	GD.Print("GM is alive 0");
		// 	_debugPrintCycleFlag=true;
		// }
		if (Input.IsActionJustPressed(_pauseKey) && IsInArena())
		{
			PausedArena1();
		}

	}

	public bool IsInArena()
	{
		bool isinarena=(CurrentGameState==EGameState.InArenaSetup) ||
		 (CurrentGameState==EGameState.InArenaPlay) ||
		 (CurrentGameState==EGameState.InArenaPause);

		return isinarena;
	}



	public void EnteredArena1()
	{

		CurrentGameState=EGameState.InArenaSetup;
		_currentArena=1;
		_player1Reference=GetNode<PlayerCharacter>("../Arena 1/Player1");
		_player2Reference=GetNode<PlayerCharacter>("../Arena 1/Player2");
		_player1Reference.SetPhysicsProcess(false);
		_player2Reference.SetPhysicsProcess(false);
		_countdownTimer = new Timer();
		AddChild(_countdownTimer);
		_countdownTimer.WaitTime=3;
		_countdownTimer.Timeout +=PlayingArena1;
		_countdownTimer.Start();

	}

	public void PlayingArena1()
	{
		_countdownTimer.QueueFree();
		_player1Reference.SetPhysicsProcess(true);
		_player2Reference.SetPhysicsProcess(true);



	}

	public void PausedArena1()
	{
		_player1Reference.GetParent().SetPhysicsProcess(false);
		_player1Reference.SetPhysicsProcess(false);
		_player2Reference.SetPhysicsProcess(false);
		var pause = GetNode("/root/BaseNode/Arena 1/UI/Pause Menu") as pause_menu;
		pause.toggle_paused();

	}

	public void UnpausedArena1()
	{
		_player1Reference.GetParent().SetPhysicsProcess(true);
		_player1Reference.SetPhysicsProcess(true);
		_player2Reference.SetPhysicsProcess(true);
	}

	public void LoopMusic()
	{
		_loopMusic.Play();
	}
}
