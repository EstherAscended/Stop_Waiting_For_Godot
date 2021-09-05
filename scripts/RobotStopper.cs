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
    }

    public void OnEnterDespawn(Area2D area)
    {
        GD.Print("Despawn entered");
        Robot.ResolveButtonDecision(gameManager.CurrentRobot);
        gameManager.CurrentRobot.DespawnRobot(1f);
        gameManager.CurrentRobot = gameManager.LoadNextRobot();
    }
}
