using System;
using Godot;
using MPAutoChess.logic.core.game;
using MPAutoChess.logic.core.placement;
using MPAutoChess.logic.core.unit;

namespace MPAutoChess.logic.core.player;

public partial class PlayerController : Node2D {
    
    public static Player CurrentPlayer { get; private set; }

    public static event Action<Unit> OnDragStart = unit => { };
    public static event Action<Unit, UnitDropTarget?, Vector2> OnDragProcess = (unit, target, position) => { };
    public static event Action<Unit> OnDragEnd = unit => { };
    
    private UnitInstance? unitInstanceUnderMouse;
    private UnitDropTarget? unitDropTargetUnderMouse;
    private Vector2 unitDropPositionUnderMouse;
    
    public Unit CurrentlyDraggedUnit { get; private set; }

    public override void _Input(InputEvent @event) {
        if (@event is InputEventMouseButton mouseButtonEvent) {
            if (mouseButtonEvent.ButtonIndex == MouseButton.Left) {
                if (unitInstanceUnderMouse != null) OnDragStart(unitInstanceUnderMouse.Unit);
            }
        }
    }

    public override void _Process(double delta) {
        CheckUnderMouse();
        if (CurrentlyDraggedUnit != null) OnDragProcess(CurrentlyDraggedUnit, unitDropTargetUnderMouse, unitDropPositionUnderMouse);
    }

    private void CheckUnderMouse() {
        Vector2 mousePosition = GetViewport().GetMousePosition();
        PhysicsDirectSpaceState2D spaceState = GetWorld2D().DirectSpaceState;
        PhysicsPointQueryParameters2D queryParameters = new PhysicsPointQueryParameters2D {
            Position = mousePosition,
            CollideWithAreas = true,
            CollisionMask = (uint) (CollisionLayers.PassiveUnitInstance | CollisionLayers.UnitDropTarget)
        };

        unitDropPositionUnderMouse = mousePosition; // only not relative if no drop target is under mouse
        foreach (IntersectionHit2D intersectionHit in spaceState.IntersectPointTyped(queryParameters)) {
            if (intersectionHit.Collider.Obj is UnitDropTarget dropTarget) {
                unitDropTargetUnderMouse = dropTarget;
                unitDropPositionUnderMouse = intersectionHit.Collider.As<Node2D>().ToLocal(mousePosition);
            } else if (intersectionHit.Collider.Obj is UnitInstance unitInstance) {
                unitInstanceUnderMouse = unitInstance;
            }
        }
    }
}