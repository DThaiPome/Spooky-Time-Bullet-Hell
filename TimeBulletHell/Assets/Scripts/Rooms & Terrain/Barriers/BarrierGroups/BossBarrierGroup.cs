using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarrierGroup : ABarrierGroup
{
    protected override void start()
    {
        base.start();
        EventManager.instance.onBossDefeatedEvent += this.removeBarriers;
    }

    protected override void onDestroy()
    {
        EventManager.instance.onBossDefeatedEvent -= this.removeBarriers;
    }
}
