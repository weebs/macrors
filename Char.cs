using Godot;
using System;

public partial class Char : CharacterBody2D
{
    private NavigationAgent2D agent;

    public Sprite2D sprite;
    public float movementSpeed = 200f;
    public float health = 50.0f;
    ProgressBar healthBar;
    Node2D highlight;
    bool isMouseOver;

    public bool IsAlive { get => Visible == true; }

    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite2D");
        healthBar = GetNode<ProgressBar>("ProgressBar");
        highlight = GetNode<Node2D>("Highlight");
        agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
        agent.PathDesiredDistance = 4;
        agent.TargetDesiredDistance = 4;
        Main.global.RegisterCharacter(this);
        MouseEntered += () => { isMouseOver = true; highlight.Visible = true; };
        MouseExited += () => { isMouseOver = false; if (!Selected) highlight.Visible = false; };
    }

    public void NavTo(Vector2 pos)
    {
        if (!IsAlive) return;
        agent.TargetPosition = pos;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!IsAlive) return;
        if (health <= 0)
        {
            Ko();
        }
        healthBar.Value = health;
        if (agent.IsNavigationFinished())
            return;
        var curPos = GlobalTransform.Origin;
        var nextPathPos = agent.GetNextPathPosition();
        Velocity = curPos.DirectionTo(nextPathPos) * movementSpeed;
        MoveAndSlide();
    }

    public void Hurt(float dmg) => health -= dmg;
    public void Ko()
    {
        if (!IsAlive) return;
        Visible = false;
        DisableMode = DisableModeEnum.Remove;
    }

    public bool Selected
    {
        get;
        set {
            field = value;
            GetNode<Node2D>("Highlight").Visible = field;
        }
    }

    public void Select() => Selected = true;
    public void Deselect() => Selected = false;

    //public void Selected()
    //{
    //    selected = true;
    //    GetNode<Node2D>("Highlight").Visible = true;
    //}

    //public void Deselected()
    //{
    //    GetNode<Node2D>("Highlight").Visible = false;
    //}

    public override void _Input(InputEvent @event)
    {
        if (isMouseOver && @event is InputEventMouseButton mb && mb.Pressed && mb.ButtonIndex == MouseButton.Left)
        {
            GetViewport().SetInputAsHandled();
            Main.global.SelectChar(this);
        }
    }
}
