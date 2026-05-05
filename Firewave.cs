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
        if (tick % 100 == 0)
        {
            var f = fireball.Instantiate<Fireball>();
            GetTree().Root.AddChild(f);
            f.Position = Position;
        }
    }
}
