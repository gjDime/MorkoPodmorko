using Godot;
using System;
using System.Reflection;

public partial class Left_Goal : Area2D
{
    // Called when the node enters the scene tree for the first time.
    private CharacterBody2D ball { get; set; }
    private Label lblCPU { get; set; }
    int CPU_Score;

    public override void _Ready()
    {
        CPU_Score = 0;
        ball = this.GetParent().GetNode<CharacterBody2D>("Pong_Ball");
        lblCPU = this.GetParent().GetNode<Label>("CPU_Score");
    }

    private void BodyEntered(Node2D node)
    {
        if (node == ball)
        {
            CPU_Score++;
            lblCPU.Text = CPU_Score.ToString();
            resetScene();
        }
        if (CPU_Score >= 5)
        {
            GetTree().ChangeSceneToFile("res://scenes/GameOver_Pong.tscn");
        }
    }

    private void resetScene()
    {
        ball.Position = new Vector2(1557, 414);
        ball.Velocity = new Vector2(-150, 150);

    }
}