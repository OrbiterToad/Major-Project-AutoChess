using Godot;

namespace MPAutoChess.logic.core.unit;

public abstract partial class Spell : Node {
    
    public abstract void Cast(UnitInstance caster);
    
    protected float GetFromLevelArray(Unit caster, float[] array) {
        if (caster.Level < array.Length) {
            return array[caster.Level];
        } else {
            float lastDif = array[^1] - array[^2];
            return array[^1] + lastDif * (caster.Level + 1 - array.Length);
        }
    }
}