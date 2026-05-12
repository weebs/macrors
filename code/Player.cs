// ReSharper disable AccessToStaticMemberViaDerivedType

using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// public union Foo(string, int);

//private T Node<T>() where T : class
//{
//    var name = typeof(T).Name;
//    return GetNode<T>(typeof(T).Name);
//}

public partial class Main : Node2D
{
    Char selectedChar
    {
        get;
        set
        {
            if (field != null)
                field.Deselect();
            field = value;
            value.Select();
        }
    }

    List<Char> chars = new();
    public IReadOnlyList<Char> Characters { get => chars; }
    public static Main global;
    int score = 0;
    Label selectedLabel;

    public void Score(int amt)
    {
        score += amt;
    }

    public override void _EnterTree()
    {
        global = this;
        GD.Print("yo");
    }

    public override void _Ready()
    {
        selectedLabel = GetNode<Label>("Label");
    }

    public void RegisterCharacter(Char c)
    {
        chars.Add(c);
        if (selectedChar == null)
            selectedChar = c;
        var btn = new TextureButton
        {
            StretchMode = TextureButton.StretchModeEnum.Scale,
            TextureNormal = c.sprite.Texture,
            IgnoreTextureSize = true,
            CustomMaximumSize = new Vector2(20, 20),
            CustomMinimumSize = new Vector2(20, 20)
        };
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
            selectedLabel.Text += ", " + score;
        }
    }

    public override async void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton me)
        {
            //await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
            if (me.ButtonIndex == MouseButton.Left && me.Pressed)
            {
                GD.Print("Nav To");
                selectedChar.NavTo(GetGlobalMousePosition());
            }
        }
    }

    public void SelectChar(Char c)
    {
        selectedChar = c;
    }
}
