using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnderPickup : APickup
{
    protected override void onPickup()
    {
        base.onPickup();
        EventManager.instance.endLevel();
    }
}

