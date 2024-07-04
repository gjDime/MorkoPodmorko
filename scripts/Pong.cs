using Godot;
using System;

public partial class Pong : Node
{
	Control gameOver;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameOver = this.GetNode<Control>("GameOverScene");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

        var global = GetNode<global_var>("/root/GlobalVar");
		if (global.pongGameOver == true)
			gameOver.Visible = true;
    }
}
