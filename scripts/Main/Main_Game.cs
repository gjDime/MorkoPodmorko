using Godot;
using System;

public partial class Main_Game : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var global = GetNode<global_var>("/root/GlobalVar");
        var purpleFishSprite = GetNode<AnimatedSprite2D>("PurpleFish/AnimatedSprite2D2");
        var blueFishSpirte = GetNode<AnimatedSprite2D>("BlueFish/AnimatedSprite2D2");
        if (!global.purpleFish)
        {
            purpleFishSprite.FlipV = true;
            purpleFishSprite.Frame = 1;
        }

        if (!global.blueFish)//TODO
        {
            blueFishSpirte.FlipV = true;
            blueFishSpirte.Frame = 1;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}