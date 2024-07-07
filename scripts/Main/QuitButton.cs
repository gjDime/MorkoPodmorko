using Godot;
using System;

public partial class QuitButton : Button
{
	// Called when the node enters the scene tree for the first time.
	private void Pressed()
	{
		GetTree().Quit();
	}
}
