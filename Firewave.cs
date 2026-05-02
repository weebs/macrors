using Godot;
using System;

public partial class Firewave : Node2D
{
    [Export]
    public PackedScene fireball;

    int tick = 0;

    public override void _PhysicsProcess(double delta)
    {
        tick++;
        if (tick % 10 == 0)
        {
            var f = fireball.Instantiate<Node2D>();
            GetTree().Root.AddChild(f);
            f.Position = Position;
        }
    }
}
