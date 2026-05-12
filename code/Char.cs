using Godot;

public partial class Char : CharacterBody2D {
    private NavigationAgent2D agent;

    public Sprite2D sprite;
    public float movementSpeed = 200f;
    public float health = 50.0f;
    ProgressBar healthBar;
    Node2D highlight;
    bool isMouseOver;

    public bool IsAlive => Visible;

    public bool Selected {
        get;
        set {
            field = value;
            highlight.Visible = field;
        }
    }

    public void Select() => Selected = true;
    public void Deselect() => Selected = false;

    public bool Hover {
        get;
        set {
            field = value;
            if (field) highlight.Visible = true;
            else if (!Selected) highlight.Visible = false;
        }
    }

    public override void _Ready() {
        print("Char!");
        sprite = GetNode<Sprite2D>("Sprite2D");
        healthBar = GetNode<ProgressBar>("ProgressBar");
        highlight = GetNode<Node2D>("Highlight");
        agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
        agent.PathDesiredDistance = 4;
        agent.TargetDesiredDistance = 4;
        G.player.RegisterCharacter(this);
        MouseEntered += () => Hover = true;
        MouseExited += () => Hover = false;
    }

    public void NavTo(Vector2 pos) {
        if (!IsAlive) return;
        agent.TargetPosition = pos;
    }

    public override void _PhysicsProcess(double delta) {
        if (!IsAlive) return;
        if (health <= 0) {
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

    public void Ko() {
        if (!IsAlive) return;
        Visible = false;
        DisableMode = DisableModeEnum.Remove;
    }

    public override void _Input(InputEvent @event) {
        if (isMouseOver && @event is InputEventMouseButton { Pressed: true, ButtonIndex: MouseButton.Left }) {
            GetViewport().SetInputAsHandled();
            G.player.SelectChar(this);
        }
    }
}