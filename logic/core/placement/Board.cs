using System;
using System.Collections.Generic;
using System.Numerics;
using Godot;
using MPAutoChess.logic.core.player;
using MPAutoChess.logic.core.stats;
using Vector2 = Godot.Vector2;

namespace MPAutoChess.logic.core.placement;

public partial class Board : Node2D, UnitContainer {

    public Player Player { get; private set; }

    private List<unit.Unit> units = new List<unit.Unit>();
    private Dictionary<unit.Unit, Vector2> placements = new Dictionary<unit.Unit, Vector2>();

    public Board(Player player) {
        Player = player;
    }

    public BoardSearch Search() {
        return new BoardSearch(this);
    }
    
    public Player GetPlayer() {
        return Player;
    }
    
    private static bool DoesOverlap(Vector2 aPos, Vector2 aSize, Vector2 bPos, Vector2 bSize) {
        return aPos.X < bPos.X + bSize.X && aPos.X + aSize.X > bPos.X && aPos.Y < bPos.Y + bSize.Y && aPos.Y + aSize.Y > bPos.Y;
    }

    public unit.Unit? GetUnitAt(Vector2 position) {
        foreach (unit.Unit unit in units) {
            Vector2 placement = placements[unit];
            if (DoesOverlap(position, Vector2.Zero, placement, unit.GetSize())) {
                return unit;
            }
        }
        return null; // No unit found at the given position
    }

    public Vector2 GetPlacement(unit.Unit unit) {
        return placements.TryGetValue(unit, out Vector2 placement) ? placement : Vector2.One * -1; // Return a negative value if the unit is not placed
    }

    public bool CanFitAt(unit.Unit unit, Vector2 pos, unit.Unit? replacedUnit = null) {
        // slot count check
        int freedSlots = replacedUnit?.Type.SlotsNeeded ?? 0;
        int requiredSlots = unit.Type.SlotsNeeded;
        int newSlotCount = units.Count + requiredSlots - freedSlots;
        if (Player.BoardSize.Evaluate() < newSlotCount) {
            return false;
        }
        
        // collision check
        foreach (unit.Unit boardUnit in units) {
            if (boardUnit == replacedUnit) {
                continue;
            }
            Vector2 placement = placements[boardUnit];
            if (DoesOverlap(pos, unit.GetSize(), placement, boardUnit.GetSize())) {
                return false;
            }
        }
        return true;
    }

    public Vector2 RemoveUnit(unit.Unit unit) {
        unit.Container = null;
        units.Remove(unit);
        Vector2 placement = placements.TryGetValue(unit, out Vector2 placement2) ? placement2 : Vector2.One * -1;
        placements.Remove(unit);
        return placement;
    }
    public void AddUnit(unit.Unit unit, Vector2 position) {
        position = new Vector2(Mathf.Round(position.X), Mathf.Round(position.Y));
        placements[unit] = position;
        units.Add(unit);
        unit.Container = this;
    }

    public bool IsValidDrop(unit.Unit unit, Vector2 pos) {
        return unit.CanBePlacedAt(this, pos);
    }
    
    public void OnUnitDrop(unit.Unit unit, Vector2 pos) {
        if (!unit.CanBePlacedAt(this, pos)) {
            GD.PrintErr("Invalid drop for unit at position: " + pos);
        }
        
        unit.Unit? existingUnit = GetUnitAt(pos);
        if (existingUnit != null) {
            units.Remove(existingUnit);
            placements.Remove(existingUnit);
        }

        UnitContainer prevContainer = unit.Container;
        Vector2 prevPlacement = prevContainer.RemoveUnit(unit);
        if (existingUnit != null) RemoveUnit(existingUnit);
        AddUnit(unit, pos);
        if (existingUnit != null) prevContainer.AddUnit(existingUnit, prevPlacement);
    }
}