using Godot;
using System;

public class Robot : KinematicBody2D
{
    public Sprite RobotHead;
    public Sprite RobotBody;
    public int RobotInt = 0;
    public string[] Answers = new string[6];
    public bool CanRobotMove = true;
    public bool FlaggedForDestroy = false;
    
    //Whether or not a robot is sentient will be determined by loading the Robot or SentientRobot scene
    [Export] public bool IsSentient = false;
    
    private int robotOptions = 7;
    private int sentientAnswerCount = 0;
    private Node2D spawnPos, centerPos, despawnPos;
    
    private RandomNumberGenerator rand = new RandomNumberGenerator();

    public override void _EnterTree()
    {
        RobotHead = GetNode<Sprite>("./Head");
        RobotBody = GetNode<Sprite>("./Body");
        GenerateRobot();
    }
    
    public override void _Ready()
    {
        spawnPos = GetNode<Node2D>("../AnchorPoints/RobotSpawn");
        centerPos = GetNode<Node2D>("../AnchorPoints/RobotCenter");
        despawnPos = GetNode<Node2D>("../AnchorPoints/RobotDespawn");
        Position = spawnPos.Position;
        
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
        if (CanRobotMove)
        {
            this.Position = Lerp(spawnPos.Position, centerPos.Position, delta);
        } 
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
        if (robotInt < 40) return Questions.LoyalAnswers[answerIndex + (10 * questionNo)];
        else
        {
            int percent = GetRand(100) + 1;
            if (percent < robotInt && sentientAnswerCount <= 3)
            {
                sentientAnswerCount++;
                return Questions.SmartAnswers[answerIndex + (10 * questionNo)];
            }
            else if (percent < 90 && sentientAnswerCount <= 3)
            {
                sentientAnswerCount++;
                return Questions.DumbAnswers[answerIndex + (10 * questionNo)];
            }
            else return Questions.LoyalAnswers[answerIndex + (10 * questionNo)];
        }
    }

    public async void DespawnRobot(float time)
    {
        await ToSignal(GetTree().CreateTimer(time), "timeout");
        GD.Print("Destroyed: " + this.Name);
        QueueFree();
    }

    private int GetRand(int n)
    {
        GameManager.Rand.Randomize();
        return GameManager.Rand.RandiRange(0, n - 1);
    }
    
    private Vector2 Lerp(Vector2 firstVector, Vector2 secondVector, float delta)
    {
        Vector2 direction = firstVector.DirectionTo(secondVector);
        Vector2 movement = direction * 1000 * delta;
        return this.Position + movement;
    }

    public static void ResolveButtonDecision(Robot robot, AnimatedSprite monitorAnim, AudioStreamPlayer monitorCorrect, AudioStreamPlayer monitorIncorrect)
    {
        if (robot.IsSentient && robot.FlaggedForDestroy)
        {
            GD.Print("Correct, destroyed sentient");
            monitorAnim.Play("tick");
            monitorCorrect.Play();
            GameManager.RobotsCorrect++;
        }
        else if (!robot.IsSentient && robot.FlaggedForDestroy)
        {
            GD.Print("Incorrect, destroyed loyal");
            monitorAnim.Play("cross");
            monitorIncorrect.Play();
            GameManager.RobotsIncorrect++;
        }
        else if (robot.IsSentient && !robot.FlaggedForDestroy)
        {
            GD.Print("Incorrect, freed sentient");
            GameManager.RobotsIncorrect++;
            monitorAnim.Play("cross");
            monitorIncorrect.Play();
            GameManager.SentientRobotsFreed++;
        }
        else if (!robot.IsSentient && !robot.FlaggedForDestroy)
        {
            GD.Print("Correct, freed loyal");
            monitorAnim.Play("tick");
            monitorCorrect.Play();
            GameManager.RobotsCorrect++;
        }
    }
}
