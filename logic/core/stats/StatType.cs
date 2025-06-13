using Godot;

namespace MPAutoChess.logic.core.stats;

public class StatType {
    public readonly string Name;
    public readonly string Description;
    public readonly Texture2D Icon;
    
    public StatType(string name, string description, Texture2D icon) {
        Name = name;
        Description = description;
        Icon = icon;
    }
    
    public static readonly StatType MAX_HEALTH = new StatType("Max Health", "Maximum health of the unit.", null);
    public static readonly StatType MAX_MANA = new StatType("Max Mana", "The amount of Mana required to cast the spell.", null);
    public static readonly StatType STARTING_MANA = new StatType("Starting Mana", "The amount of Mana the unit starts combat with.", null);
    public static readonly StatType ARMOR = new StatType("Armor", "Reduces incoming physical damage.", null);
    public static readonly StatType AEGIS = new StatType("Aegis", "Reduces incoming magical damage.", null);
    public static readonly StatType STRENGTH = new StatType("Strength", "Damage dealt by the unit's attacks. Some spell cast are also improved by this.", null);
    public static readonly StatType MAGIC = new StatType("Magic", "Improves the units spell casts.", null);
    public static readonly StatType ATTACK_SPEED = new StatType("Attack Speed", "How often the unit attacks each second.", null);
    public static readonly StatType RANGE = new StatType("Range", "The distance at which the unit can attack.", null);
    public static readonly StatType WIDTH = new StatType("Width", "The width of the unit in grid cells.", null);
    public static readonly StatType HEIGHT = new StatType("Height", "The height of the unit in grid cells.", null);

}