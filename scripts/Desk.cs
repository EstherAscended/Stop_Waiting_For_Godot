using Godot;
using System;

public class Desk : Panel
{
    private GameManager gameManager;
    private AnimatedSprite belt, crusher;
    private AudioStreamPlayer beltSound, crushSound;
    private Texture crushedRobot;
    public override void _Ready()
    {
        gameManager = GetTree().Root.GetNode<Node2D>("Node2D").GetNode<GameManager>("GameManager");
        belt = GetNode<AnimatedSprite>("Belt");
        beltSound = belt.GetNode<AudioStreamPlayer>("BeltSound");
        crusher = GetNode<AnimatedSprite>("DoorsNode/Crusher");
        crushSound = crusher.GetNode<AudioStreamPlayer>("CrushSound");
        crushedRobot = GD.Load<Texture>("res://assets/sprites/robot/crush.png");
    }

    public void OnRobotFree()
    {
        GD.Print("free");
        belt.Play();
        beltSound.Play();
        gameManager.CurrentRobot.FlaggedForDestroy = false;
        gameManager.CurrentRobot.CanRobotMove = true;
    }
    public void OnRobotDestroy()
    {
        GD.Print("destroy");
        gameManager.CurrentRobot.FlaggedForDestroy = true;
        CrushRobotThenMove(1f);
        gameManager.CurrentRobot.RobotBody.Texture = crushedRobot;
        gameManager.CurrentRobot.RobotHead.Texture = null;
        crusher.Frame = 0;
    }

    public void OnQuestion0()
    {
        GD.Print("0");
        GD.Print(gameManager.CurrentRobot.Answers[0]);
    }
    
    public void OnQuestion1()
    {
        GD.Print("1");
        GD.Print(gameManager.CurrentRobot.Answers[1]);
    }
    
    public void OnQuestion2()
    {
        GD.Print("2");
        GD.Print(gameManager.CurrentRobot.Answers[2]);
    }
    
    public void OnQuestion3()
    {
        GD.Print("3");
        GD.Print(gameManager.CurrentRobot.Answers[3]);
    }
    
    public void OnQuestion4()
    {
        GD.Print("4");
        GD.Print(gameManager.CurrentRobot.Answers[4]);
    }
    
    public void OnQuestion5()
    {
        GD.Print("5");
        GD.Print(gameManager.CurrentRobot.Answers[5]);
    }

    public void OnStart()
    {
        SpawnFirstRobot(1f);
    }

    private async void CrushRobotThenMove(float time)
    {
        crusher.Play();
        crushSound.Play();
        await ToSignal(GetTree().CreateTimer(time), "timeout");
        belt.Play();
        beltSound.Play();
        gameManager.CurrentRobot.CanRobotMove = true;
    }

    private async void SpawnFirstRobot(float time)
    {
        await ToSignal(GetTree().CreateTimer(time), "timeout");
        GetTree().Root.GetNode<Node2D>("Node2D").AddChild(gameManager.CurrentRobot);
        belt.Play("default");
        beltSound.Play();
    }
}
