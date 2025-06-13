using Godot;
using MPAutoChess.logic.core.player;
using MPAutoChess.logic.core.unit;

namespace MPAutoChess.logic.core.shop;

public partial class Shop : Control {
    
    [Export] public Container ShopSlotContainer { get; set; }
    
    
    private void AddOffers(UnitType[] offers) {
        foreach (Node child in ShopSlotContainer.GetChildren()) {
            child.QueueFree(); // Clear existing slots
        }
        foreach (UnitType offer in offers) {
            TextureButton slot = new TextureButton();
            slot.SetTextureNormal(offer.Icon);
            slot.SetStretchMode(TextureButton.StretchModeEnum.KeepAspectCentered);
            slot.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            slot.Pressed += () => {
                if (PlayerController.CurrentPlayer.TryPurchase(offer)) {
                    slot.QueueFree();
                }
            };
            ShopSlotContainer.AddChild(slot);
        }
    }
    
}