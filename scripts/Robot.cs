using Godot;
using System;

public class Robot : KinematicBody2D
{
    public Sprite RobotHead;
    public Sprite RobotBody;
    public int RobotInt = 0;
    public string[] Answers = new string[6];
    
    //Whether or not a robot is sentient will be determined by loading the Robot or SentientRobot scene
    [Export] public bool IsSentient = false;
    
    private int robotOptions = 7;

    public override void _EnterTree()
    {
        RobotHead = GetNode<Sprite>("./Head");
        RobotBody = GetNode<Sprite>("./Body");
        GenerateRobot();
    }
    
    public override void _Ready()
    {
        if (IsSentient)
        {
            //set intelligence between 41 and 100 
            RobotInt = GetRand(60) + 1;
            RobotInt += 40;
        }

        for (int i = 0; i < Answers.Length; i++)
        {
            Answers[i] = ChooseAnswer(RobotInt, i);
        }
    }

    public override void _Process(float delta)
   {
      
   }

    private void GenerateRobot()
    {
        string headPath = "res://assets/sprites/robot/head/";
        string bodyPath = "res://assets/sprites/robot/body/";

        int headIndex = GetRand(robotOptions);
        int bodyIndex = GetRand(robotOptions);

        RobotHead.Texture = GD.Load<Texture>(headPath + headIndex + ".png");
        RobotBody.Texture = GD.Load<Texture>(bodyPath + bodyIndex + ".png");
    }
    private string ChooseAnswer(int robotInt, int questionNo)
    {
        int answerIndex = GetRand(10);
        if (robotInt < 40) return Questions.LoyalAnswers[answerIndex] + (10 * questionNo);
        else
        {
            int percent = GetRand(100) + 1;
            if (percent < robotInt) return Questions.SmartAnswers[answerIndex] + (10 * questionNo);
            else if (percent < 90) return Questions.DumbAnswers[answerIndex] + (10 * questionNo);
            else return Questions.LoyalAnswers[answerIndex] + (10 * questionNo);
        }
    }

    private int GetRand(int n)
    {
        return (int)GD.Randi() % n;
    }
}
