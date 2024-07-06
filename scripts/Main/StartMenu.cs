using Godot;
using System;

public partial class StartMenu : Control
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    private void PlayPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/Main/game.tscn");
    }
    private void ExitPressed()
    {
		GetTree().Quit();

    }
	private void ControlsPressed()
	{
        GetTree().ChangeSceneToFile("res://scenes/Main/Controls.tscn");
    }
}
