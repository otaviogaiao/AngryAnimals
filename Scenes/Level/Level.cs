using Godot;
using System;

public partial class Level : Node2D
{
	private PackedScene _animalScene = GD.Load<PackedScene>("res://Scenes/Animal/Animal.tscn");
	[Export] private Marker2D _startPosition;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SpawnAnimal();
		SignalManager.Instance.AnimalDied += SpawnAnimal;
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.AnimalDied -= SpawnAnimal;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Q))
		{
			GetTree().ChangeSceneToFile("res://Scenes/Main/Main.tscn");
		}
	}

	private void SpawnAnimal()
	{
		GD.Print("Spawning animal");
		Animal animal = _animalScene.Instantiate<Animal>();
		animal.Position = _startPosition.Position;
		AddChild(animal);
	}
}
