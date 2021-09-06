using Godot;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;

public class GameManager : Node
{
    public static RandomNumberGenerator Rand = new RandomNumberGenerator();
    public PackedScene[] RobotsForGame = new PackedScene[30];
    public PackedScene Robot = GD.Load<PackedScene>("res://scenes/robots/Robot.tscn");
    public PackedScene SentientRobot = GD.Load<PackedScene>("res://scenes/robots/SentientRobot.tscn");
    public Robot CurrentRobot;
    public static int RobotsCorrect, RobotsIncorrect, SentientRobotsFreed = 0;
    private Label correctLabel, incorrectLabel;

    private int nextRobotIndex = 0;
    
    [Export] public int SentientRobotsInGame = 10;
    
    public override void _EnterTree()
    {
       Questions.LoadAnswers();
       LoadRobots();
    }

    public override void _Ready()
    {
       CurrentRobot = LoadNextRobot();
       GameManager.RobotsCorrect = 0;
       GameManager.RobotsIncorrect = 0;
       GameManager.SentientRobotsFreed = 0;
       correctLabel = GetNode<Label>("../Panel/CorrectLabel");
       incorrectLabel = GetNode<Label>("../Panel/IncorrectLabel");
    }

    public override void _Process(float delta)
    {
        correctLabel.Text = GameManager.RobotsCorrect.ToString();
        incorrectLabel.Text = GameManager.RobotsIncorrect.ToString();
    }

    private void LoadRobots()
    {
        var sentientIndexes = new List<int>();
        for (int i = 0; i < SentientRobotsInGame; i++)
        {
            Rand.Randomize();
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

    public Robot LoadNextRobot()
    {
        Robot rob = RobotsForGame[nextRobotIndex].Instance<Robot>();
        if (nextRobotIndex < RobotsForGame.Length - 1) nextRobotIndex++;
        return rob;
    }

    public async void DelayTimer(float time)
    {
        await ToSignal(GetTree().CreateTimer(time), "timeout");
    }
}
