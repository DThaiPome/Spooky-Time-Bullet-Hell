using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelessGunPickup : APickup
{
    protected override InventoryItem getInventoryItem()
    {
        return new TimelessGun();
    }
}
