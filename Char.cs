using Godot;
using System;

public partial class Char : CharacterBody2D
{
    private NavigationAgent2D agent;

    public Sprite2D sprite;
    public float movementSpeed = 200f;
    public float health = 50.0f;
    ProgressBar healthBar;

    public bool IsAlive { get => Visible == true; }

    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite2D");
        healthBar = GetNode<ProgressBar>("ProgressBar");
        agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
        agent.PathDesiredDistance = 4;
        agent.TargetDesiredDistance = 4;
        Callable.From(ActorSetup).CallDeferred();
        GetParent<Main>().RegisterCharacter(this);
    }

    public void NavTo(Vector2 pos) {
        if (!IsAlive) return;
        agent.TargetPosition = pos; 
    }

    public override void _PhysicsProcess(double delta)
    {
        if (health <= 0)
        {
            Visible = false;
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

    private async void ActorSetup()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
        //agent.TargetPosition = new(70, 20);
    }

    public void Selected()
    {
        GetNode<Node2D>("Highlight").Visible = true;
    }

    public void Deselected()
    {
        GetNode<Node2D>("Highlight").Visible = false;
    }
}
