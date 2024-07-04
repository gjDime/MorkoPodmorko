using Godot;
using System;

public partial class PongController : CharacterBody2D
{
	public const float Speed = 450.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		float direction = Input.GetAxis("move_up", "move_down");
		//Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != 0)
		{
			velocity.Y = direction * Speed;
		}
		else
		{
			velocity.Y = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
