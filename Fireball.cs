using Godot;
using System;

public partial class Fireball : Area2D
{
    Vector2 dir = new(-1, -0.25f);
    float speed = 200f;
    int ticks = 0;

    public override void _Ready()
    {
        BodyEntered += bodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        ticks++;
        if (ticks > 1000) QueueFree();
        Translate(dir * speed * (float)delta);
    }

    void bodyEntered(Node2D body)
    {
        GD.Print("Fireball ", this, " collided with ", body);
        if (body is Char c)
        {
            c.Hurt(10f);
        }
        QueueFree();
    }
}
