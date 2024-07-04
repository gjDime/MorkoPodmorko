using Godot;
using System;
using static Godot.TextServer;

public partial class pong_ai : CharacterBody2D
{
    private static float movementSpeed = 500;
    private CharacterBody2D Ball {  get; set; }
// Called when the node enters the scene tree for the first time.
public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        
        float directionY = Ball.Position.Y - Position.Y;
        int directionX = calculateSideOfBoard();

       

       

       /* if (directionX == -1)
        {
            if(velocity.Y < 390)
                velocity.Y += movementSpeed;
            else if (velocity.Y > 390)
                velocity.Y = movementSpeed;
        }
        else
        {*/
            if (directionY < 0)
                velocity.Y = movementSpeed*-1;
            else if (directionY > 0)
                velocity.Y = movementSpeed;
        //  }


        


        Velocity = velocity;
        MoveAndSlide();
    }

    private int calculateSideOfBoard()
    {
        int directionX;
        if (Ball.Position.X <= 1560)
        {
            directionX = -1;
        }
        else
            directionX = 1;

        return directionX;
    }
    public override void _Ready()
    {
        Ball = GetParent().GetNode<Godot.CharacterBody2D>("Pong_Ball");
 
    }

}