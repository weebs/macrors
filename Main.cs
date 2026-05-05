// ReSharper disable AccessToStaticMemberViaDerivedType

using Godot;
using System;
using System.Collections.Generic;

public union Foo(string, int);

//private T Node<T>() where T : class
//{
//    var name = typeof(T).Name;
//    return GetNode<T>(typeof(T).Name);
//}

public partial class Main : Node2D
{
    Char selectedChar
    {
        set
        {
            if (field != null)
                field.Deselected();
            field = value;
            value.Selected();
        }
        get => field;
    }
    List<Char> chars = new();
    Label selectedLabel;
    public override void _Ready()
    {
        selectedLabel = GetNode<Label>("Label");
    }

    public void ConnectChar(Char c)
    {
        chars.Add(c);
        if (selectedChar == null)
            selectedChar = c;
        var btn = new TextureButton();
        btn.StretchMode = TextureButton.StretchModeEnum.Scale;
        btn.TextureNormal = c.sprite.Texture;
        btn.IgnoreTextureSize = true;
        btn.CustomMaximumSize = new Vector2(20, 20);
        btn.CustomMinimumSize = new Vector2(20, 20);
        btn.Pressed += () => { selectedChar = c; };
        GetNode("VBoxContainer").AddChild(btn);
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
