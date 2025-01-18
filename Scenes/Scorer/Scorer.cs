using Godot;
using System;

public partial class Scorer : Node
{
	private int _totalCups = 0;
	private int _totalCupsDestroyed = 0;
	private int _attempts = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalManager.Instance.AttemptMade += OnAttemptMade;
		SignalManager.Instance.CupDestroyed += OnCupDestroyed;
		_totalCups = GetTree().GetNodesInGroup(Cup.GroupName).Count;
		GD.Print($"Scorer found {_totalCups} cups");
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.CupDestroyed -= OnCupDestroyed;
		SignalManager.Instance.AttemptMade -= OnAttemptMade;
	}

	private void OnCupDestroyed()
	{
		_totalCupsDestroyed++;
		CheckLevelCompleted();
	}

	private void CheckLevelCompleted()
	{
		if (_totalCupsDestroyed == _totalCups)
		{
			SignalManager.Instance.EmitLevelCompleted();
			GD.Print("Level completed");
		}
	}

	private void OnAttemptMade()
	{
		_attempts++;
		SignalManager.Instance.EmitAttemptUpdated(_attempts);
	}
}
