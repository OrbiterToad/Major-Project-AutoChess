using Godot;
using MPAutoChess.logic.core.player;

namespace MPAutoChess.logic.core.game;

public partial class Game : Node {
    
    public static Game Instance { get; private set; }

    public GameMode Mode { get; set; }

    public Player[] players;

    public override void _EnterTree() {
        Instance = this;
    }

    public void Initialize(Account[] accounts) {
        players = new Player[accounts.Length];
        for (int i = 0; i < accounts.Length; i++) {
            players[i] = new Player();
            players[i].Account = accounts[i];
        }
    }

    public override void _PhysicsProcess(double delta) {
        Mode.Tick(delta);
    }
}