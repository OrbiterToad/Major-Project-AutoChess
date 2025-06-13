#nullable enable
namespace MPAutoChess.logic.core.item;

public class ItemSlot {

    private Item? item;
    
    public ItemSlot(Item? item) {
        this.item = item;
    }

    public Item? GetItem() {
        return item;
    }

    public void SetItem(Item? item) {
        this.item = item;
    }
    
}