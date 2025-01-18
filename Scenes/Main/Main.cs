using Godot;
using System;

public partial class Main : Control
{
	private PackedScene[] _levels =
	{
		GD.Load<PackedScene>("res://Scenes/Level/Level1.tscn"),
		GD.Load<PackedScene>("res://Scenes/Level/Level2.tscn"),
		GD.Load<PackedScene>("res://Scenes/Level/Level3.tscn")
	};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalManager.Instance.LevelChanged += OnLevelChanged;
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.LevelChanged -= OnLevelChanged;
	}

	private void OnLevelChanged()
	{
		GetTree().ChangeSceneToPacked(_levels[ScoreManager.GetLevelSelected() - 1]);
	}
}
