using System;
using Godot;
using MPAutoChess.logic.core.game;
using MPAutoChess.logic.core.player;
using PlayerController = MPAutoChess.logic.core.player.PlayerController;

namespace MPAutoChess.logic.core.placement;

public partial class SingleUnitSlot : Area2D, UnitContainer {
    
    [Export] public Sprite2D HoverEffect;
    [Export] public Texture2D HoverOffTexture;
    [Export] public Texture2D HoverOnTexture;
    
    public Player Player { get; set; }

    public unit.Unit? Unit { get; private set; }
    
    public void OnDragStart(unit.Unit unit) {
        HoverEffect.Texture = HoverOffTexture;
    }
    
    public void OnDragProcess(unit.Unit unit, UnitDropTarget? target, Vector2 pos) {
        if (target == this && IsValidDrop(unit, pos)) {
            HoverEffect.Texture = HoverOnTexture;
        } else {
            HoverEffect.Texture = HoverOffTexture;
        }
    }
    
    public void OnDragEnd(unit.Unit unit) {
        HoverEffect.Texture = null;
    }

    public override void _EnterTree() {
        HoverEffect.Texture = HoverOnTexture; // make sure the hover effect is not visible initially (it is visible in editor for placement)
        PlayerController.Instance.OnDragStart += OnDragStart;
        PlayerController.Instance.OnDragProcess += OnDragProcess;
        PlayerController.Instance.OnDragEnd += OnDragEnd;
    }
    
    public override void _ExitTree() {
        PlayerController.Instance.OnDragStart -= OnDragStart;
        PlayerController.Instance.OnDragProcess -= OnDragProcess;
        PlayerController.Instance.OnDragEnd -= OnDragEnd;
    }

    public bool IsValidDrop(unit.Unit unit, Vector2 pos) {
        return Unit == null || unit.CanBePlacedAt(this, pos);
    }
    
    public void OnUnitDrop(unit.Unit unit, Vector2 pos) {
        if (!IsValidDrop(unit, pos)) {
            GD.PrintErr("Invalid drop position for unit: " + unit.Type.Name);
            return;
        }

        UnitContainer prevContainer = unit.Container;
        Vector2 prevPlacement = unit.Container.RemoveUnit(unit);
        if (Unit != null) {
            unit.Unit toBeReplaced = Unit;
            RemoveUnit(Unit);
            prevContainer.AddUnit(toBeReplaced, prevPlacement);
        }
        AddUnit(unit, pos);
    }
    public Player GetPlayer() {
        return Player;
    }
    public unit.Unit GetUnitAt(Vector2 position) {
        return Unit;
    }
    public Vector2 GetPlacement(unit.Unit unit) {
        return unit == Unit ? Vector2.Zero : Vector2.One * -1; // Return a negative value if the unit is not placed
    }
    public bool CanFitAt(unit.Unit unit, Vector2 position, unit.Unit replacedUnit = null) {
        return true;
    }
    public Vector2 RemoveUnit(unit.Unit unit) {
        if (unit != Unit) {
            return Vector2.One * -1;
        }
        
        Unit.Container = null;
        Unit = null;
        return Vector2.Zero;
    }
    public void AddUnit(unit.Unit unit, Vector2 position) {
        Unit = unit;
        unit.Container = this;
        AddChild(unit.Type.UnitInstancePrefab.Instantiate());
        GD.Print("Hello");
    }
}