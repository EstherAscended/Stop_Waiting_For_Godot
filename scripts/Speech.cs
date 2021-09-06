using Godot;
using System;
using System.Collections.Generic;

public class Speech : Node2D
{
	//This is taken from a tutorial and I don't have time to change the things I want so it's essentially a copy paste job
	public string BubbleText = "Testing text";
	private bool canShrink = true;
    private int bubbleTextIndex = 0;
    private List<char> currentText = new List<char>();
    private Timer timer;
    private NinePatchRect nineRect;
    private Label labelText;
	private bool doClose = false;
	private Desk desk;

    public override void _Ready()
    {
	    desk = GetTree().Root.GetNode<Desk>("Node2D/Panel");
	    labelText = GetNode<Label>("VBoxContainer/Label");
	    nineRect = GetNode<NinePatchRect>("VBoxContainer/Label/NinePatchRect");
	    timer = GetNode<Timer>("Timer");
	    timer.Start(1);
    }

    private void OnTimerTimeout()
    {
	    if (!doClose)
	    {
		    currentText.Add(BubbleText[bubbleTextIndex]);
		    labelText.Text = new string(currentText.ToArray());

		    if (bubbleTextIndex < BubbleText.Length - 1)
		    {
			    timer.Start(0.05f);
			    bubbleTextIndex++;
		    }
		    else
		    {
			    if (!doClose)
			    {
				    doClose = true;
				    timer.Start(1);
			    }		    
		    }
	    }
	    else
	    {
		    if (currentText.Count > 0)
		    {
			    currentText.RemoveAt(currentText.Count-1);
				labelText.Text = new string(currentText.ToArray());
			    if (canShrink)
			    {
				    nineRect.RectSize -= new Vector2(6, 0);
				    nineRect.RectPosition += new Vector2(3, 0);
			    }
			    timer.Start(0.001f);
		    }
		    else
		    {
			    desk.CanPressButton = true;
			    QueueFree();
		    }
	    }
    }
}