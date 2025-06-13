using System;
using Godot;
using MPAutoChess.logic.core.stats;
using MPAutoChess.logic.core.unit;

namespace MPAutoChess.logic.season0.spells.epic;

[GlobalClass, Tool]
public partial class MageSpell : Spell {
    
    private float[] BaseDamage { get; set; } = { 300, 500, 2000 };
    private float[] DamageScaling { get; set; } = { 5, 5, 10 };
    
    private float GetDamageAmount(UnitInstance caster) {
        return GetFromLevelArray(caster.Unit, BaseDamage) + caster.Stats.GetValue(StatType.MAGIC) * GetFromLevelArray(caster.Unit, DamageScaling);
    } 

    public override void Cast(UnitInstance caster) {
        throw new NotImplementedException();
    }
}