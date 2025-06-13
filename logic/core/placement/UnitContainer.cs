using System.Collections.Generic;
using Godot;
using MPAutoChess.logic.core.player;

namespace MPAutoChess.logic.core.placement;

public interface UnitContainer : UnitDropTarget {
    
    public Player GetPlayer();
    
    public unit.Unit? GetUnitAt(Vector2 position);
    
    public Vector2 GetPlacement(unit.Unit unit);
    
    public bool CanFitAt(unit.Unit unit, Vector2 position, unit.Unit? replacedUnit = null);
    
    public Vector2 RemoveUnit(unit.Unit unit);
    
    public void AddUnit(unit.Unit unit, Vector2 position);
    
}