using Godot;
using System;

public partial class table : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Card card = new Card();
		
		// Sprite2D sprite = card.GetNode<Godot.Sprite2D>("Sprite2D");
		// String imgPath = "res://assets/cards/1h.png";
		// Image img = new Image();
		// img.Load(imgPath);
		// sprite.Texture = ImageTexture.CreateFromImage(img);
		
		AddChild(card);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
