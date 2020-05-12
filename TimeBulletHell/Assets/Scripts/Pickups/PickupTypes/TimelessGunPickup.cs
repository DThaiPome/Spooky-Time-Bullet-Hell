using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelessGunPickup : InventoryPickup
{
    protected override InventoryItem getInventoryItem()
    {
        return new TimelessGun();
    }
}
