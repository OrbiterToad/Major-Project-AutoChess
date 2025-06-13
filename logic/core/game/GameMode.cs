using Godot;

namespace MPAutoChess.logic.core.game;

public interface GameMode {

    public void CreateUserInterface(Node2D parent);
    
    public void Tick(double delta);

    public int GetPhase();

    public string GetPhaseName();

}