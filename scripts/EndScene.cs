using Godot;
using System;

public class EndScene : Control
{
    private Label releaseLoyal, releaseSentient, crushLoyal, crushSentient;

    public override void _Ready()
    {
        releaseLoyal = GetNode<Label>("Panel/ReleaseLoyal");
        releaseSentient = GetNode<Label>("Panel/ReleaseSentient");
        crushLoyal = GetNode<Label>("Panel/CrushLoyal");
        crushSentient = GetNode<Label>("Panel/CrushSentient");
    }
    public override void _Process(float delta)
    {
        releaseLoyal.Text = $"You released {GameManager.LoyalRobotsFreed} loyal robots";
        releaseSentient.Text = $"You released {GameManager.SentientRobotsFreed} sentient robots";
        crushLoyal.Text = $"You crushed {GameManager.LoyalRobotsCrushed} loyal robots";
        crushSentient.Text = $"You crushed {GameManager.SentientRobotsCrushed} sentient robots";
    }

}
