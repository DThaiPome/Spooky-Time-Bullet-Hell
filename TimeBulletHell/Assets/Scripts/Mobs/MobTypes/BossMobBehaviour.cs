using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMobBehaviour : MobBehaviour
{
    protected override void onDeath()
    {
        base.onDeath();
        this.beforeLevelEnds();
        this.endLevel();
    }

    protected virtual void beforeLevelEnds()
    {
        Debug.Log("wow");
    }

    protected virtual void endLevel()
    {
        Debug.Log("u did it we r so proud of u");
    }
}
