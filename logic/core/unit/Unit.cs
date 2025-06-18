using Godot;
using MPAutoChess.logic.core.placement;
using MPAutoChess.logic.core.stats;

namespace MPAutoChess.logic.core.unit;

public class Unit {
    
    public Stats Stats { get; private set; } = new Stats();
    
    public uint Level { get; private set; } = 1;
    
    public UnitType Type { get; private set; }
    
    public UnitContainer Container { get; set; }
    
    public Unit(UnitType type) {
        Type = type;
        // TODO: Load stats from type
    }

    public float GetStatValue(StatType statType) {
        return Stats.GetValue(statType);
    }

    public Vector2 GetSize() {
        return new Vector2(Stats.GetValue(StatType.WIDTH), Stats.GetValue(StatType.HEIGHT));
    }

    public bool CanBePlacedAt(UnitContainer unitContainer, Vector2 position) {
        if (unitContainer == Container) {
            Unit? existingUnit = unitContainer.GetUnitAt(position);
            return unitContainer.CanFitAt(this, position, existingUnit);
        } 

        Unit? existingUnit = unitContainer.GetUnitAt(position);
        bool thisCanFit = unitContainer.CanFitAt(this, position, existingUnit);
        bool existingCanFit = existingUnit == null || Container.CanFitAt(existingUnit, Container.GetPlacement(this), this);
        return thisCanFit && existingCanFit;
    }
}