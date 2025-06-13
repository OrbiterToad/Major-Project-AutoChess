using Godot;
using Godot.Collections;
using MPAutoChess.logic.core.placement;
using MPAutoChess.logic.core.stats;
using MPAutoChess.logic.core.unit;

namespace MPAutoChess.logic.core.player;

public partial class Player : Node {

    public Account Account { get; set; }

    public int Health { get; private set; } = 1000;

    public int Experience { get; private set; } = 0;

    public int Gold { get; private set; } = 100;
    
    public Board Board { get; private set; }
    
    [Export] public Bench Bench { get; private set; }
    
    public Calculation BoardSize { get; private set; } = new Calculation(1);

    public Player() {
        Board = new Board(this);
    }
    
    public bool TryPurchase(UnitType unitType) {
        SingleUnitSlot benchSlot = Bench.GetFirstFreeSlot();
        if (benchSlot == null) return false;
        if (Gold >= unitType.Cost) {
            Gold -= unitType.Cost;
            benchSlot.AddUnit(new Unit(unitType), Vector2.Zero);
            return true;
        }
        return false;
    }
    
    public void MoveToTemporaryBench(unit.Unit unit) {
        // TODO: Implement logic to move the unit to a temporary bench
    }

}