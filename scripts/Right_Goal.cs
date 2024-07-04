using Godot;
using System;

public partial class Right_Goal : Area2D
{
    // Called when the node enters the scene tree for the first time.
    private CharacterBody2D ball { get; set; }
    private CharacterBody2D player { get; set; }
    private CharacterBody2D CPU { get; set; }
    private Label lblPlayer {  get; set; }
    int Player_Score;

    public override void _Ready()
    {
        Player_Score = 0;
        player = this.GetParent().GetNode<CharacterBody2D>("Player_Pong");
        CPU = this.GetParent().GetNode<CharacterBody2D>("Pong_AI");

        ball = this.GetParent().GetNode<CharacterBody2D>("Pong_Ball");
        lblPlayer = this.GetParent().GetNode<Label>("Player_Score");
    }

    private void BodyEntered(Node2D node)
    {
        if (node == ball)
        {
            Player_Score++;
            lblPlayer.Text = Player_Score.ToString();
            resetScene();
        }  
        if (Player_Score >= 3)
        {
            var global = GetNode<global_var>("/root/GlobalVar");
            global.blueFish = false;
            GetTree().ChangeSceneToFile("res://scenes/game.tscn");
        }

    }
    
    private void resetScene()
    {
        ball.Position = new Vector2(1557,414);
        ball.Velocity = new Vector2(-150, 150);

    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
