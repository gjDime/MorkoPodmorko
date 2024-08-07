using Godot;
using System;
using System.ComponentModel;

public partial class Pong_ball : CharacterBody2D
{
    private Vector2 velocity;
    private AudioStreamPlayer2D asp;
	private float _bounceFactor = 1.0f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        velocity = new Vector2(-210, 210);
        asp = this.GetParent().GetNode<AudioStreamPlayer2D>("Impact");
    }
    public override void _PhysicsProcess(double delta)
    {
		
        var collision_info = MoveAndCollide(velocity*(float)delta);
		if (collision_info != null)
		{
        

            velocity = velocity.Bounce(collision_info.GetNormal());
            PlaySound();
           

        }

		Position += velocity * (float)delta;

    }

    private void PlaySound()
    {
        var sound = ResourceLoader.Load<AudioStreamOggVorbis>("res://assets/audio/pong/impact.ogg");
        asp.Stream = sound;
        asp.Play();
    }


















	private void AdjustVelocityBasedOnAngle(Vector2 collisionNormal)
    {

        float angleOfCollision = Mathf.RadToDeg(Mathf.Acos(collisionNormal.Dot(Vector2.Up)));

      
        if (angleOfCollision < 30)
        {
            _bounceFactor = 1.0f; 
        }
        else if (angleOfCollision < 60)
        {
            _bounceFactor = 1.0f; 
        }
        else
        {
            _bounceFactor = 0.8f; 
        }

        
    }
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.

}
