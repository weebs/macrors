using Godot;
using System;
using System.Linq;

public partial class Firewave : Node2D
{
    [Export]
    public PackedScene fireball;
    Random r = new();

    int tick = 0;

    public override void _PhysicsProcess(double delta)
    {
        tick++;
        if (tick % 100 == 0)
        {
            var f = fireball.Instantiate<Fireball>();
            f.Position = Position;

            var m = GetNode<Main>("/root/Main");
            var alive = m.Characters.Where(c => c.IsAlive).ToArray();
            var random = alive[r.Next(0, alive.Length - 1)];
            f.dir = (random.Position - f.Position).Normalized();

            GetTree().Root.AddChild(f);
        }
    }
}
