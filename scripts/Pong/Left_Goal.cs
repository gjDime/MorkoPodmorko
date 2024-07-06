using Godot;
using System;
using System.Reflection;

public partial class Left_Goal : Area2D
{
    // Called when the node enters the scene tree for the first time.
    private CharacterBody2D ball { get; set; }
    private CharacterBody2D player { get; set; }
    private CharacterBody2D CPU { get; set; }
    private Label lblCPU { get; set; }
    int CPU_Score;

    public override void _Ready()
    {
        CPU_Score = 0;
        player = this.GetParent().GetNode<CharacterBody2D>("Player_Pong");
        CPU = this.GetParent().GetNode<CharacterBody2D>("Pong_AI");
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
        if (CPU_Score >= 3)
        {
            var global = GetNode<global_var>("/root/GlobalVar");
            global.pongGameOver = true;
            GetTree().Paused = true; 
        }
    }

    private void resetScene()
    {
        ball.Position = new Vector2(1557, 414);
        ball.Velocity = new Vector2(-210, 210);
        player.Position = new Vector2(player.Position.X, player.Position.Y);
        CPU.Position = new Vector2(CPU.Position.X, CPU.Position.Y);
    }
}