﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMobBehaviour : MobBehaviour
{
    protected override void onDeath()
    {
        base.onDeath();
        this.beforeLevelEnds();
    }

    protected virtual void beforeLevelEnds()
    {
        EventManager.instance.onBossDefeated();
        Debug.Log("wow");
        this.endLevel();
    }

    protected virtual void endLevel()
    {
        EventManager.instance.endLevel();
        Debug.Log("u did it we r so proud of u");
    }
}
