using Godot;
using System;

public partial class SignalManager : Node
{
	public static SignalManager Instance { get; private set; }

	[Signal]
	public delegate void AttemptMadeEventHandler();
	
	[Signal]
	public delegate void AnimalDiedEventHandler();
	
	[Signal]
	public delegate void CupDestroyedEventHandler();

	[Signal]
	public delegate void LevelChangedEventHandler();

	[Signal]
	public delegate void LevelCompletedEventHandler();

	[Signal]
	public delegate void AttemptUpdatedEventHandler(int numOfAttempts);
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = new SignalManager();
	}

	public void EmitAnimalDied()
	{
		EmitSignal(SignalName.AnimalDied);
	}

	public void EmitCupDestroyed()
	{
		EmitSignal(SignalName.CupDestroyed);
	}

	public void EmitLevelChanged()
	{
		EmitSignal(SignalName.LevelChanged);
	}

	public void EmitLevelCompleted()
	{
		EmitSignal(SignalName.LevelCompleted);
	}

	public void EmitAttemptMade()
	{
		EmitSignal(SignalName.AttemptMade);
	}

	public void EmitAttemptUpdated(int numOfAttempts)
	{
		EmitSignal(SignalName.AttemptUpdated, numOfAttempts);
	}
}
