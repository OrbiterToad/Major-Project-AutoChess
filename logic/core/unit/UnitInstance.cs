using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using MPAutoChess.logic.core.stats;

namespace MPAutoChess.logic.core.unit;

[GlobalClass, Tool]
public partial class UnitInstance : CharacterBody2D {
    
    public Unit Unit { get; private set; }
    public Stats Stats { get; private set; } = new Stats();
    public float CurrentHealth { get; private set; }
    public float CurrentMana { get; private set; }
    
    public AnimatedSprite2D Sprite { get; private set; }
    public bool IsCombatUnit { get; private set; }
    
    public void Heal(float amount) {
        if (amount <= 0) return;
        CurrentHealth = Math.Min(CurrentHealth + amount, Stats.GetValue(StatType.MAX_HEALTH));
    }

    public override string[] _GetConfigurationWarnings() {
        GD.Print("_GetConfigurationWarnings");
        
        List<string> warnings = new List<string>();
        Array<Node> children = GetChildren();
        if (!children.Any(child => child is AnimatedSprite2D)) {
            warnings.Add("UnitInstance must have an AnimatedSprite2D as a child.");
        }
        if (children.Count(child => child is Spell) != 1) {
            warnings.Add("UnitInstance must have exactly one Spell as a child.");
        }
        
        return warnings.ToArray();
    }
}