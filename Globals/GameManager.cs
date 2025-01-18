using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	public static void LoadMain()
	{
		Instance.GetTree().ChangeSceneToFile("res://scenes/Main/Main.tscn");
	}
}
