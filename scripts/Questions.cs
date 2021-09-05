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
       
        /*
        var loyalTxt = ResourceLoader.Load<File>(path + "LoyalAnswers.txt");
        var dumbTxt = ResourceLoader.Load<File>(path + "DumbAnswers.txt");
        var smartTxt = ResourceLoader.Load<File>(path + "SmartAnswers.txt");
        */
        var loyalTxt = new File();
        var dumbTxt = new File();
        var smartTxt = new File();
        
        loyalTxt.Open($"{path}LoyalAnswers.txt", File.ModeFlags.Read);
        dumbTxt.Open($"{path}DumbAnswers.txt", File.ModeFlags.Read);
        smartTxt.Open($"{path}SmartAnswers.txt", File.ModeFlags.Read);
        
        /*
        loyalTxt.Open(loyalTxt.GetPath(), File.ModeFlags.Read);
        dumbTxt.Open(dumbTxt.GetPath(), File.ModeFlags.Read);
        smartTxt.Open(smartTxt.GetPath(), File.ModeFlags.Read);
        */
        
        for (int i = 0; i < LoyalAnswers.Length; i++)
        {
            LoyalAnswers[i] = loyalTxt.GetLine();
            DumbAnswers[i] = dumbTxt.GetLine();
            SmartAnswers[i] = smartTxt.GetLine();
        }
        
        loyalTxt.Close();
        dumbTxt.Close();
        smartTxt.Close();
    }
    
}
