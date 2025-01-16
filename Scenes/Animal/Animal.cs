using Godot;
using System;

public partial class Animal : RigidBody2D
{
	public enum AnimalState
	{
		Ready,
		Drag,
		Release
	}
	
	private static readonly Vector2 DragLimMax = new (0, 60);
	private static readonly Vector2 DragLimMin = new (-60, 0);

	private const float ImpulseMult = 20.0f;
	private const float ImpulseMax = 1200.0f;
	
	[Export] private AudioStreamPlayer2D _stretchSound;
	[Export] private AudioStreamPlayer2D _kickSound;
	[Export] private AudioStreamPlayer2D _launchSound;

	[Export] private Sprite2D _arrow;
	[Export] private VisibleOnScreenNotifier2D _visibleOnScreen;
	[Export] private Label _debugLabel;
	
	private AnimalState _state = AnimalState.Ready;
	
	private Vector2 _startPosition = Vector2.Zero;
	private Vector2 _dragStart = Vector2.Zero;
	private Vector2 _draggedVector = Vector2.Zero;
	private Vector2 _lastDraggedVector = Vector2.Zero;
	private float _arrowScaleX;

	private bool _stretchSoundPlayed = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ConnectSignals();
		InitializeVariables();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		UpdateState();
		UpdateDebugLabel();
	}

	private void UpdateDebugLabel()
	{
		_debugLabel.Text = $"St:{_state.ToString()} Sl:{Sleeping}";
		_debugLabel.Text += $"_dragStart:{_dragStart.X:F1}, {_dragStart.Y:F1}";
	}

	private void ConnectSignals()
	{
		InputEvent += OnInputEvent;
		SleepingStateChanged += OnSleepStateChanged;
		_visibleOnScreen.ScreenExited += OnScreenExited;
	}

	private void InitializeVariables()
	{
		_arrowScaleX = _arrow.Scale.X;
		_startPosition = Position;
		_arrow.Hide();
	}

	private void StartDragging()
	{
		_dragStart = GetGlobalMousePosition();
		_arrow.Show();
	}

	private Vector2 CalculateImpulse()
	{
		return _draggedVector * (- ImpulseMult);
	}

	private void StartRelease()
	{
		_arrow.Hide();
		_launchSound.Play();
		Freeze = false;
		_stretchSoundPlayed = false;
		ApplyCentralImpulse(CalculateImpulse());
	}

	private void ConstraintDragWithinLimits()
	{
		_lastDraggedVector = _draggedVector;
		_draggedVector = _draggedVector.Clamp(DragLimMin, DragLimMax);
		Position = _startPosition + _draggedVector;
	}

	private void PlayStretchSound()
	{	
		Vector2 diff = _draggedVector - _lastDraggedVector;
		if (diff.Length() > 0 && !_stretchSoundPlayed)
		{
			_stretchSoundPlayed = true;
			_stretchSound.Play();
		}
	}

	private void UpdateDraggedVector()
	{
		_draggedVector = GetGlobalMousePosition() - _dragStart;
	}

	private bool DetectRelease()
	{
		if (Input.IsActionJustReleased("drag") && _state == AnimalState.Drag)
		{
			ChangeState(AnimalState.Release);
			return true;
		}
		return false;
	}

	private void UpdateArrowScale()
	{
		var impulseLength = CalculateImpulse().Length();
		var scalePercentage = impulseLength / ImpulseMax;
		_arrow.Scale = new Vector2(
			(_arrowScaleX * scalePercentage) + _arrowScaleX, _arrow.Scale.Y);
		_arrow.Rotation = ((_startPosition - Position).Angle());
	}
	
	private void HandleDragging()
	{
		if (DetectRelease())
			return;
		UpdateDraggedVector();
		PlayStretchSound();
		ConstraintDragWithinLimits();
		UpdateArrowScale();
	}

	private void UpdateState()
	{
		switch (_state)
		{
			case AnimalState.Drag:
				HandleDragging();
				break;
			case AnimalState.Release:
				break;
		}
	}

	private void ChangeState(AnimalState newState)
	{
		_state = newState;
		switch (_state)
		{
			case AnimalState.Drag:
				StartDragging();
				break;
			case AnimalState.Release:
				StartRelease();
				break;
		}
	}

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (_state == AnimalState.Ready && @event.IsActionPressed("drag"))
		{
			ChangeState(AnimalState.Drag);
		} else if (_state == AnimalState.Drag && @event.IsActionReleased("drag"))
		{
			ChangeState(AnimalState.Release);
		}
	}

	private void OnSleepStateChanged()
	{
		GD.Print("OnSleepStateChanged");
	}
	
	private void OnScreenExited()
	{
		GD.Print("OnScreenExited");
		QueueFree();
	}
}
