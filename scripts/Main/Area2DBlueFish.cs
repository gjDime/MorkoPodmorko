using Godot;
using System;

public partial class Area2DBlueFish : Area2D
{
	private bool entered = false;
	// Called when the node enters the scene tree for the first time.

	private void BodyEntered(Node2D node)
	{
		if(node.Name == "Player")
		entered = true;
		// GD.Print(entered);
	}
	
	
	private void BodyExcited(Node2D node)
	{
		if(node.Name == "Player")
		entered = false;
		// GD.Print(entered);
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (entered)
		{
			if (Input.IsActionPressed("Interact"))
			{
				GetTree().ChangeSceneToFile("res://scenes/Pong_Game.tscn");
			}
		}
	}
}
