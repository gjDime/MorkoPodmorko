using Godot;
using System;

public partial class pong_ai : CharacterBody2D
{
	private static float movementSpeed = 200;
	
	// Called when the node enters the scene tree for the first time.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		CharacterBody2D Ball = GetParent().GetNode<Godot.CharacterBody2D>("Pong_Ball");
		float direction = Ball.Position.Y - Position.Y;

		if(direction < 0)
			velocity.Y -= movementSpeed;
		else
			velocity.Y += movementSpeed;
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
