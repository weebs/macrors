using Godot;
using System;
using System.Collections.Generic;
// ReSharper disable AccessToStaticMemberViaDerivedType

public union Foo(string, int);

//private T Node<T>() where T : class
//{
//    var name = typeof(T).Name;
//    return GetNode<T>(typeof(T).Name);
//}

public partial class Main : Node2D
{
    Char selectedChar;
    //Char char1;
    //Char char2;
    //Char char3;
    //Char char4;
    List<Char> chars = new();
    Label selectedLabel;
    public override void _Ready()
    {
        //char1 = GetNode<Char>("Char");
        //char2 = GetNode<Char>("Char2");
        //char3 = GetNode<Char>("Char3");
        //char4 = GetNode<Char>("Char4");
        selectedLabel = GetNode<Label>("Label");
        GetNode<Button>("Button").Connect(Button.SignalName.Pressed, Callable.From(Button1Clicked));
        GetNode<Button>("Button2").Connect(Button.SignalName.Pressed, Callable.From(Button2Clicked));
        GetNode<Button>("Button3").Connect(Button.SignalName.Pressed, Callable.From(Button3Clicked));
        GetNode<Button>("Button4").Connect(Button.SignalName.Pressed, Callable.From(Button4Clicked));
        //selectedChar = char1;
        //GD.Print(char1);
    }

    public void ConnectChar(Char c)
    {
        chars.Add(c);
        if (selectedChar == null)
            selectedChar = c;
    }

    void Button1Clicked()
    {
        selectedChar = chars[0];
        //selectedChar = char1;
    }
    void Button2Clicked()
    {
        selectedChar = chars[1];
        //selectedChar = char2;
    }
    void Button3Clicked()
    {
        selectedChar = chars[2];
        //selectedChar = char3;
    }
    void Button4Clicked()
    {
        selectedChar = chars[3];
        //selectedChar = char4;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Char1"))
            selectedChar = chars[0];
        else if (Input.IsActionJustPressed("Char2"))
            selectedChar = chars[1];
        else if (Input.IsActionJustPressed("Char3"))
            selectedChar = chars[2];
        else if (Input.IsActionJustPressed("Char4"))
            selectedChar = chars[3];
    }

    public override void _PhysicsProcess(double delta)
    {
        if (selectedChar != null)
        {
            selectedLabel.Text = selectedChar.Name;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton me)
        {
            if (me.ButtonIndex == MouseButton.Left && me.Pressed)
            {
                selectedChar.NavTo(GetGlobalMousePosition());
            }
        }
    }
}
