using Godot;
using System;
using System.Reflection;

public partial class Left_Goal : Area2D
{
    // Called when the node enters the scene tree for the first time.
    private CharacterBody2D ball { get; set; }

    public override void _Ready()
    {
        ball = this.GetParent().GetNode<CharacterBody2D>("Pong_Ball");
    }

    private void BodyEntered(Node2D node)
    {
        if (node == ball)
            GetTree().ChangeSceneToFile("res://scenes/Pong_Game.tscn");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}