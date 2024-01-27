using Godot;
using System;

public partial class GameManager : Node
{

	private bool _debugPrintCycleFlag = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("GM is online");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_debugPrintCycleFlag)
		{
			GD.Print("GM is alive 1");
			_debugPrintCycleFlag=false;
		}
		else
		{
			GD.Print("GM is alive 0");
			_debugPrintCycleFlag=true;
		}

	}
}
