using Godot;
using System;

public class SceneManager : Node
{
    private void LoadMainMenu()
    {
        GetTree().ChangeScene("res://scenes/MainMenu.tscn");
    }
    
    private void LoadTutorial()
    {
        GetTree().ChangeScene("res://scenes/Tutorial.tscn");
    }
    
    private void LoadGame()
    {
        GetTree().ChangeScene("res://scenes/Level.tscn");
    }
}
