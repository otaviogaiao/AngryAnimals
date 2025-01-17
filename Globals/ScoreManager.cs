using Godot;
using System;

public partial class ScoreManager : Node
{
	public static ScoreManager Instance { get; private set; }
	
	private const int DefaultScore = 1000;
	private int _levelSelected;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	public static int GetLevelBestScore(int level)
	{
		return DefaultScore;
	}

	public static int GetLevelSelected()
	{
		return Instance._levelSelected;
	}

	public static int SetLevelSelected(int level)
	{
		return Instance._levelSelected = level;
	}
}
