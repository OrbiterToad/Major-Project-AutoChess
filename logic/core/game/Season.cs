using Godot;
using MPAutoChess.logic.core.unit;

namespace MPAutoChess.logic.core.game;

public abstract partial class Season : Node {
    
    [Export] public UnitCollection Units { get; set; }
    
}