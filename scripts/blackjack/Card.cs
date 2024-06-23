using Godot;
using System;
using System.Collections.Generic;

public partial class Card : StaticBody2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Dictionary<string, int> cards = new Dictionary<string, int>();
        
        
        
        
        
        Sprite2D sprite = GetNode<Godot.Sprite2D>("Sprite2D");
        String imgPath = "res://assets/cards/1h.png";
        
        Image img = new Image();
        Error err = img.Load(imgPath);

        // if (err != Error.Ok)
        // {
        //     GD.PrintErr("Failed to load image: ", imgPath);
        //     return;
        // }

        sprite.Texture = ImageTexture.CreateFromImage(img);
    }

    
    
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        
    }
}