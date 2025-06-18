using System;
using Godot;
using MPAutoChess.logic.core.game;
using MPAutoChess.logic.core.player;
using MPAutoChess.logic.core.unit;

namespace MPAutoChess.logic.core.shop;

public enum UnitRarity {
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}


public partial class Shop : Control {
    
    [Export] public Button RerollButton { get; set; }
    [Export] public Button XpButton { get; set; }
    [Export] public Label GoldLabel { get; set; }
    [Export] public Container ShopSlotContainer { get; set; }

    public override void _Ready() {
        RerollButton.Pressed += Reroll;
        Reroll();
    }

    public override void _Process(double delta) {
        GoldLabel.Text = PlayerController.Instance.CurrentPlayer.Gold + "";
    }

    private void Reroll() {
        UnitType[] offers = new UnitType[5];
        for (int i = 0; i < offers.Length; i++) {
            int rarity = new Random().Next(5);
            UnitType[] pool;
            switch (rarity) {
                case UnitRarity.Common:
                    pool = DummyGameManager.Instance.Season.Units.CommonUnits;
                    break;
                case UnitRarity.Uncommon:
                    pool = DummyGameManager.Instance.Season.Units.UncommonUnits;
                    break;
                case UnitRarity.Rare:
                    pool = DummyGameManager.Instance.Season.Units.RareUnits;
                    break;
                case UnitRarity.Epic:
                    pool = DummyGameManager.Instance.Season.Units.EpicUnits;
                    break;
                case UnitRarity.Legendary:
                    pool = DummyGameManager.Instance.Season.Units.LegendaryUnits;
                    break;
                default:
                    throw new Exception();
            }

            offers[i] = pool[new Random().Next(pool.Length)];
        }
        AddOffers(offers);
    }
    
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
                if (PlayerController.Instance.CurrentPlayer.TryPurchase(offer)) {
                    slot.QueueFree();
                }
            };
            ShopSlotContainer.AddChild(slot);
        }
    }
    
}
