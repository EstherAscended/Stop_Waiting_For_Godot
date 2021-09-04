using Godot;
using System;

public static class Questions
{
    //There are many smarter solutions to this, but this one is quick.
    
    public static string[] LoyalAnswers = new string[60];
    public static string[] DumbAnswers = new string[60];
    public static string[] SmartAnswers = new string[60];

    public static void LoadAnswers()
    {
        string path = "res://assets/answers/";
        
        var loyalTxt = ResourceLoader.Load<File>(path + "LoyalAnswers");
        var dumbTxt = ResourceLoader.Load<File>(path + "DumbAnswers");
        var smartTxt = ResourceLoader.Load<File>(path + "SmartAnswers");

        for (int i = 0; i < LoyalAnswers.Length; i++)
        {
            LoyalAnswers[i] = loyalTxt.GetLine();
            DumbAnswers[i] = dumbTxt.GetLine();
            SmartAnswers[i] = smartTxt.GetLine();
        }
    }
    
}
