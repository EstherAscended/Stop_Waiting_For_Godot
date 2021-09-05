using Godot;
using System;

public class GameManager : Node
{
    public static RandomNumberGenerator Rand = new RandomNumberGenerator();
    
    public override void _EnterTree()
    {
       Questions.LoadAnswers(); 
    }
}
