using Godot;
using System;

public partial class player : CharacterBody2D
{
    [Export] public const float Speed = 750.0f;
    [Export] public float maxXSpeed = 400f;
    [Export] public float maxYSpeed = 250f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        Sprite2D sprite = this.GetNode<Godot.Sprite2D>("Sprite"); //    MUCHO IMPORTANTE!!!!!
        Camera2D camera = this.GetNode<Godot.Camera2D>("Camera2D");

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        if (direction != Vector2.Zero)
        {
            
            if (Math.Abs(velocity.X) <= maxXSpeed)
                velocity.X += direction.X * Speed * (float)delta;
            

            if (Math.Abs(velocity.Y) <= maxYSpeed)
                velocity.Y += direction.Y * Speed * (float)delta;

            sprite.FlipH = velocity.X >= 0;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, 13);
            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, 13);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}