using Godot;
using System;

public class RobotStopper : Node
{
    private GameManager gameManager;
    private AnimatedSprite belt, monitorAnim; 
    private AudioStreamPlayer beltSound, monitorCorrect, monitorIncorrect;
    private Desk desk;
    
    public override void _Ready()
    {
        gameManager = GetTree().Root.GetNode<Node2D>("Node2D").GetNode<GameManager>("GameManager");
        desk = GetNode<Desk>("../Panel");
        belt = GetNode<AnimatedSprite>("../Panel/Belt");
        monitorAnim = GetNode<AnimatedSprite>("../Panel/Desk/Monitor/MonitorAnim");
        monitorCorrect = GetNode<AudioStreamPlayer>("../Panel/Desk/Monitor/MonitorCorrect");
        monitorIncorrect = GetNode<AudioStreamPlayer>("../Panel/Desk/Monitor/MonitorIncorrect");
        beltSound = belt.GetNode<AudioStreamPlayer>("BeltSound");
    }

    public void RobotEntered(Area2D area)
    {
        gameManager.CurrentRobot.CanRobotMove = false;
        monitorAnim.Play("default");
        belt.Stop();
        belt.Frame = 0;
        desk.CanPressButton = true;
    }

    public void OnEnterDespawn(Area2D area)
    {
        GD.Print("Despawn entered");
        belt.Stop();
        belt.Frame = 0;
        Robot.ResolveButtonDecision(gameManager.CurrentRobot, monitorAnim, monitorCorrect, monitorIncorrect);
        gameManager.CurrentRobot.DespawnRobot(1f);
        gameManager.CurrentRobot = gameManager.LoadNextRobot();
        GetTree().Root.GetNode<Node2D>("Node2D").AddChild(gameManager.CurrentRobot);
        belt.Play("default");
        beltSound.Play();
    }
}
