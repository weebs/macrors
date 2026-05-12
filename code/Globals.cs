global using static Globals;
using Godot;

public static class Globals {
    public static void print(string s) {
        GD.Print(s);
    }

    public static class G {
        public static Player player { get; set; }
    }
}