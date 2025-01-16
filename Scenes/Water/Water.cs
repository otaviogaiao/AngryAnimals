using Godot;
using System;

public partial class Water : Area2D
{
	[Export] private AudioStreamPlayer2D _splashSound;
	[Export] private CollisionShape2D _collisionShape;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnAreaEntered;
	}
	
	private void OnAreaEntered(Node2D body2D)
	{
		_splashSound.Play();
	}
}
