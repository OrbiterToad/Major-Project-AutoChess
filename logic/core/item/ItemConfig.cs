using Godot;

namespace MPAutoChess.logic.core.item;

[GlobalClass]
public partial class ItemConfig : Resource {
    
    [Export] public ItemType[] ItemTypes { get; set; }

    public ItemType? GetRecipeFor(ItemType typeA, ItemType typeB) {
        foreach (ItemType itemType in ItemTypes) {
            if (itemType.CraftedFromA == typeA && itemType.CraftedFromB == typeB) return itemType;
            if (itemType.CraftedFromA == typeB && itemType.CraftedFromB == typeA) return itemType;
        }

        return null;
    }
    
}