using Godot;
using System;

public partial class Cup : StaticBody2D
{
	public const String GroupName = "cup";
	
	[Export] private AnimationPlayer _vanishAnimation;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_vanishAnimation.AnimationFinished += OnAnimationFinished;
	}

	public void Die()
	{
		_vanishAnimation.Play("vanish");
	}

	private void OnAnimationFinished(StringName animationName)
	{
		SignalManager.Instance.EmitCupDestroyed();
		QueueFree();
	}
}
