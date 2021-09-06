using Godot;
using System;

public class MusicManager : Node
{
    private AudioStreamPlayer music;
    
    public override void _Ready()
    {
        music = GetNode<AudioStreamPlayer>("Music");
        music.Play();
    }
}
