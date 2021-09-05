using Godot;
using System;

public class Desk : Panel
{
    private GameManager gameManager;
    private AnimatedSprite belt;
    public override void _Ready()
    {
        gameManager = GetTree().Root.GetNode<Node2D>("Node2D").GetNode<GameManager>("GameManager");
        belt = GetNode<AnimatedSprite>("Belt");
    }

    public void OnRobotFree()
    {
        GD.Print("free");
        belt.Play();
        gameManager.CurrentRobot.FlaggedForDestroy = false;
        gameManager.CurrentRobot.CanRobotMove = true;
    }
    public void OnRobotDestroy()
    {
        GD.Print("destroy");
        belt.Play();
        gameManager.CurrentRobot.FlaggedForDestroy = true;
        gameManager.CurrentRobot.CanRobotMove = true;  
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
        GetTree().Root.GetNode<Node2D>("Node2D").AddChild(gameManager.CurrentRobot);
        belt.Play("default");
    }
}
