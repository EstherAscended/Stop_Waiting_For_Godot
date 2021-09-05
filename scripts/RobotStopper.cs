using Godot;
using System;

public class RobotStopper : Node
{
    private GameManager gameManager;
    private AnimatedSprite belt;
    
    public override void _Ready()
    {
        gameManager = GetTree().Root.GetNode<Node2D>("Node2D").GetNode<GameManager>("GameManager");
        belt = GetNode<AnimatedSprite>("../Panel/Belt");
    }

    public void RobotEntered(Area2D area)
    {
        gameManager.CurrentRobot.CanRobotMove = false;
        belt.Stop();
        belt.Frame = 0;
    }

    public void OnEnterDespawn(Area2D area)
    {
        GD.Print("Despawn entered");
        belt.Stop();
        belt.Frame = 0;
        Robot.ResolveButtonDecision(gameManager.CurrentRobot);
        gameManager.CurrentRobot.DespawnRobot(1f);
        gameManager.CurrentRobot = gameManager.LoadNextRobot();
    }
}
