using Godot;
using System;

public partial class GameUi : Control
{
	[Export] private Label _levelLabel;
	[Export] private Label _attemptsLabel;

	[Export] private VBoxContainer _levelCompletedContainer;
	[Export] private AudioStreamPlayer _gameOverSound;

	private bool _gameOver = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalManager.Instance.AttemptUpdated += OnAttemptUpdated;
		SignalManager.Instance.LevelCompleted += OnLevelCompleted;
		_levelLabel.Text = $"LEVEL {ScoreManager.GetLevelSelected()}";
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.AttemptUpdated -= OnAttemptUpdated;
		SignalManager.Instance.LevelCompleted -= OnLevelCompleted;
	}

	public override void _Process(double delta)
	{
		if (_gameOver && Input.IsKeyPressed(Key.Space))
		{
			GameManager.LoadMain();
		}
	}

	private void OnAttemptUpdated(int score)
	{
		_attemptsLabel.Text = $"ATTEMPTS {score}";
	}

	private void OnLevelCompleted()
	{
		_levelCompletedContainer.Visible = true;
		_gameOverSound.Play();
		_gameOver = true;
	}
}
