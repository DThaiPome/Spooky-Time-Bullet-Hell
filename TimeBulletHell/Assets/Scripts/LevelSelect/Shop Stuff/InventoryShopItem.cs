using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryShopItem", menuName = "ShopItems/InventoryShopItem", order = 1)]
public class InventoryShopItem : AShopItem
{
    [SerializeField]
    protected InventoryShopItems item;

    public override bool tryToPurchase()
    {
        this.ableToPurchase();
        return true;
    }

    protected override void onPurchase()
    {
        base.onPurchase();
        EventManager.instance.addToInventory(this.enumToItem());
    }

    protected override bool ableToPurchase()
    {
        EventManager.instance.returnInventoryFullEvent += this.inventoryFullOnPurchase;
        EventManager.instance.returnInventoryNotFullEvent += this.inventoryNotFullOnPurchase;
        EventManager.instance.queryInventoryAvailability(this);

        return true;
    }

    protected virtual void inventoryNotFullOnPurchase(System.Object o)
    {
        if (o.Equals(this))
        {
            this.purchaseIfAffordable();

            EventManager.instance.returnInventoryFullEvent -= this.inventoryFullOnPurchase;
            EventManager.instance.returnInventoryNotFullEvent -= this.inventoryNotFullOnPurchase;
        }
    }

    protected virtual void inventoryFullOnPurchase(System.Object o)
    {
        if (o.Equals(this))
        {
            EventManager.instance.returnInventoryFullEvent -= this.inventoryFullOnPurchase;
            EventManager.instance.returnInventoryNotFullEvent -= this.inventoryNotFullOnPurchase;
        }
    }

    protected virtual InventoryItem enumToItem()
    {
        switch(this.item)
        {
            case InventoryShopItems.TimelessGun:
                return new TimelessGun();
            default:
                return null;
        }
    }
}

public enum InventoryShopItems
{
    TimelessGun
}
