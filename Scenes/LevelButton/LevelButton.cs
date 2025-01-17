using Godot;
using System;

public partial class LevelButton : TextureButton
{
	[Export] private int _levelNumber;
	[Export] private Label _levelLabel;
	[Export] private Label _scoreLabel;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_levelLabel.Text = _levelNumber.ToString();
		_scoreLabel.Text = ScoreManager.GetLevelBestScore(_levelNumber).ToString("D4");
		Pressed += OnPressed;
		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
	}

	private void OnPressed()
	{
		ScoreManager.SetLevelSelected(_levelNumber);
		GD.Print("Level " + ScoreManager.GetLevelSelected());
	}

	private void OnMouseEntered()
	{
		SetScale(new Vector2(1.1f, 1.1f));
	}

	private void OnMouseExited()
	{
		SetScale(new Vector2(1f, 1f));
	}
}
