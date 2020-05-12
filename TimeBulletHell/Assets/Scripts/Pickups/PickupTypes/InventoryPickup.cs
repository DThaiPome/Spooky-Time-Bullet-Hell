using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryPickup : APickup
{
    protected override void onPickup()
    {
        base.onPickup();
        EventManager.instance.onInventoryItemCollected(this);
    }

    public virtual void addToInventory()
    {
        EventManager.instance.addToInventory(this.getInventoryItem());
        this.gameObject.SetActive(false);
    }

    protected abstract InventoryItem getInventoryItem();
}
