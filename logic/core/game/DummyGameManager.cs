using Godot;
using MPAutoChess.logic.core.player;

namespace MPAutoChess.logic.core.game;

[GlobalClass]
public partial class DummyGameManager : Node {
    
    [Export] public Season Season { get; set; }

    public static DummyGameManager Instance { get; private set; }

    public override void _Ready() {
        Instance = this;
    }

    public void Start() {
        Game game = new Game();
        game.Mode = new EchoMode();
        AddChild(game);
        
        Account[] accounts = new Account[8];
        for (int i = 0; i < 8; i++) {
            accounts[i] = new Account("Player " + (i + 1));
        }
        
        
        game.Initialize(accounts);
    }
}