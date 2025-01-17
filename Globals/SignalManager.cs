using Godot;
using System;

public partial class SignalManager : Node
{
	public static SignalManager Instance { get; private set; }

	[Signal]
	public delegate void AnimalDiedEventHandler();
	
	[Signal]
	public delegate void CupDestroyedEventHandler();
	
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
}
