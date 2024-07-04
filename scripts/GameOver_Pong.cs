using Godot;
using System;

public partial class GameOver_Pong : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	private void RetryPressed()
	{
        GetTree().ChangeSceneToFile("res://scenes/Pong_Game.tscn");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
