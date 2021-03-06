﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMobBehaviour : MobBehaviour
{
    [SerializeField]
    private ABarrierGroup barriers;
    [SerializeField]
    private Vector2 levelEnderPosition;

    protected override void onEnable()
    {
        base.onEnable();

        if (this.barriers != null)
        {
            this.barriers.setBarriers();
        }
    }

    protected override void onDeath()
    {
        base.onDeath();
        this.beforeLevelEnds();
    }

    protected virtual void beforeLevelEnds()
    {
        EventManager.instance.onBossDefeated();
        PickupManager.instance.spawn("LevelEnderPickup", new DefaultPickupSpawnProperties(levelEnderPosition));
        //this.endLevel();
    }

    protected virtual void endLevel()
    {
        EventManager.instance.endLevel();
    }
}
