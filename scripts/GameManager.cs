using Godot;
using System;
using System.Collections.Generic;

public class GameManager : Node
{
    public static RandomNumberGenerator Rand = new RandomNumberGenerator();
    public PackedScene[] RobotsForGame = new PackedScene[30];
    public PackedScene Robot = GD.Load<PackedScene>("res://scenes/robots/Robot.tscn");
    public PackedScene SentientRobot = GD.Load<PackedScene>("res://scenes/robots/SentientRobot.tscn");
    
    [Export] public int SentientRobotsInGame = 10;
    
    public override void _EnterTree()
    {
       Questions.LoadAnswers();
       LoadRobots();
    }

    public override void _Ready()
    {
    }

    private void LoadRobots()
    {
        var sentientIndexes = new List<int>();
        for (int i = 0; i < SentientRobotsInGame; i++)
        {
            int rand = Rand.RandiRange(0, RobotsForGame.Length - 1);
            if (!sentientIndexes.Contains(rand))
            {
                sentientIndexes.Add(rand);
            }
            else
            {
                i--;
            }
        }

        for (int i = 0; i < RobotsForGame.Length; i++)
        {
            if (sentientIndexes.Contains(i)) RobotsForGame[i] = SentientRobot;
            else
            {
                RobotsForGame[i] = Robot;
            }

        }
    }

    public async void DelayTimer(float time)
    {
        await ToSignal(GetTree().CreateTimer(time), "timeout");
    }
}
