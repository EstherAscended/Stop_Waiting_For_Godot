using Godot;
using System;

public class RobotStopper : Node
{
    private GameManager gameManager;
    
    public override void _Ready()
    {
        gameManager = GetTree().Root.GetNode<Node2D>("Node2D").GetNode<GameManager>("GameManager");
    }

    public void RobotEntered(Area2D area)
    {
        gameManager.CurrentRobot.CanRobotMove = false;
        GD.Print("entered");
    }
}
