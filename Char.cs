using Godot;
using System;

public partial class Char : CharacterBody2D
{
    private NavigationAgent2D agent;

    public float movementSpeed = 200f;

    public override void _Ready()
    {
        agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
        agent.PathDesiredDistance = 4;
        agent.TargetDesiredDistance = 4;
        Callable.From(ActorSetup).CallDeferred();
    }

    public void NavTo(Vector2 pos) { agent.TargetPosition = pos; }

    public override void _PhysicsProcess(double delta)
    {
        if (agent.IsNavigationFinished())
            return;
        var curPos = GlobalTransform.Origin;
        var nextPathPos = agent.GetNextPathPosition();
        Velocity = curPos.DirectionTo(nextPathPos) * movementSpeed;
        MoveAndSlide();
    }

    private async void ActorSetup()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
        //agent.TargetPosition = new(70, 20);
    }
}
