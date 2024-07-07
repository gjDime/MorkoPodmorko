using Godot;
using System;

public partial class Main_Game : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var global = GetNode<global_var>("/root/GlobalVar");
        var purpleFishSprite = GetNode<AnimatedSprite2D>("PurpleFish/AnimatedSprite2D2");
        var blueFishSprite = GetNode<AnimatedSprite2D>("BlueFish/AnimatedSprite2D2");
        
        if (!global.purpleFish)
        {
            purpleFishSprite.FlipV = true;
            purpleFishSprite.Frame = 1;
            purpleFishSprite.Stop();
        }

        if (!global.blueFish)
        {
            blueFishSprite.FlipV = true;
            blueFishSprite.Frame = 1;
            blueFishSprite.Stop();
        }

        if (!global.blueFish && !global.purpleFish)
        {
            GetTree().ChangeSceneToFile("res://scenes/Main/end_scene.tscn");
        }
    }
}