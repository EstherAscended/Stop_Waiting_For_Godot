using Godot;
using System;

public class Desk : Panel
{
    private GameManager gameManager;
    private AnimatedSprite belt, crusher;
    private AudioStreamPlayer beltSound, crushSound, qSound, buttonSound;
    private Texture crushedRobot;
    private PackedScene speechBubble;
    private Button startButton;
    public bool CanPressButton = false;
    public override void _Ready()
    {
        gameManager = GetTree().Root.GetNode<Node2D>("Node2D").GetNode<GameManager>("GameManager");
        belt = GetNode<AnimatedSprite>("Belt");
        beltSound = belt.GetNode<AudioStreamPlayer>("BeltSound");
        crusher = GetNode<AnimatedSprite>("DoorsNode/Crusher");
        crushSound = crusher.GetNode<AudioStreamPlayer>("CrushSound");
        crushedRobot = GD.Load<Texture>("res://assets/sprites/robot/crush.png");
        qSound = GetNode<AudioStreamPlayer>("Desk/QSound");
        buttonSound = GetNode<AudioStreamPlayer>("Desk/ButtonSound");
        startButton = GetNode<Button>("Desk/Start");
        speechBubble = GD.Load<PackedScene>("res://scenes/Speech.tscn");
    }

    public void OnRobotFree()
    {
        if (CanPressButton)
        {
            GD.Print("free");
            belt.Play();
            beltSound.Play();
            buttonSound.Play();
            gameManager.CurrentRobot.FlaggedForDestroy = false;
            gameManager.CurrentRobot.CanRobotMove = true;
            CanPressButton = false;
        }
    }
    public void OnRobotDestroy()
    {
        if (CanPressButton)
        {
            GD.Print("destroy");
            gameManager.CurrentRobot.FlaggedForDestroy = true;
            buttonSound.Play();
            CrushRobotThenMove(1f);
            gameManager.CurrentRobot.RobotBody.Texture = crushedRobot;
            gameManager.CurrentRobot.RobotHead.Texture = null;
            crusher.Frame = 0;
            CanPressButton = false;
        }
    }

    public void OnQuestion0()
    {
        if (CanPressButton)
        {
            HandleAfterPress(0);
        }
    }
    
    public void OnQuestion1()
    {
        if (CanPressButton)
        {
           HandleAfterPress(1);
        }
    }
    
    public void OnQuestion2()
    {
        if (CanPressButton)
        {
           HandleAfterPress(2);
        }
    }
    
    public void OnQuestion3()
    {
        if (CanPressButton)
        {
           HandleAfterPress(3);
        }
    }
    
    public void OnQuestion4()
    {
        if (CanPressButton)
        {
           HandleAfterPress(4);
        }
    }
    
    public void OnQuestion5()
    {
        if (CanPressButton)
        {
           HandleAfterPress(5);
        }
    }

    public void OnStart()
    {
        SpawnFirstRobot(1f);
        startButton.Disabled = true;
        startButton.Visible = false;
        qSound.Play();
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

    private void HandleAfterPress(int qIndex)
    {
        CanPressButton = false;
        qSound.Play();
        Speech qBubble = speechBubble.Instance<Speech>();
        qBubble.BubbleText = gameManager.CurrentRobot.Answers[qIndex];
        gameManager.CurrentRobot.AddChild(qBubble);
    }
}
