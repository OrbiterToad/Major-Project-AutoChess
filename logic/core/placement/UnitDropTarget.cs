using Godot;

namespace MPAutoChess.logic.core.placement;

public interface UnitDropTarget {
    
    public bool IsValidDrop(unit.Unit unit, Vector2 pos);
    
    public void OnUnitDrop(unit.Unit unit, Vector2 pos);

}