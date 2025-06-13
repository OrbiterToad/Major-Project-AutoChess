namespace MPAutoChess.logic.core.item;

public class Item {

    public ItemType Type { get; set; }

    public Item(ItemType type) {
        Type = type;
    }
    
}