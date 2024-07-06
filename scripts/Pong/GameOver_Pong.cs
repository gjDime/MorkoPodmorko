using Godot;
using System;

public partial class GameOver_Pong : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Visible = false;
	}

	private void RetryPressed()
	{
        var global = GetNode<global_var>("/root/GlobalVar");
		global.pongGameOver = false;
		this.Visible = false;
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://scenes/Pong/Pong.tscn");
		
    }
	private void GoBackPressed()
	{
        var global = GetNode<global_var>("/root/GlobalVar");
        global.pongGameOver = false;
        this.Visible = false;
        GetTree().ChangeSceneToFile("res://scenes/game.tscn");
    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
