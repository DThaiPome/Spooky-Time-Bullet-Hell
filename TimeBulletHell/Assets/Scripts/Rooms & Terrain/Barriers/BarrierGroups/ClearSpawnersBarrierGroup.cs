using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSpawnersBarrierGroup : ABarrierGroup
{
    [SerializeField]
    protected List<GameObject> spawnerObjects;

    protected List<MobSpawner> spawners;

    protected override void start()
    {
        base.start();
        this.spawners = new List<MobSpawner>();
        foreach(GameObject g in this.spawnerObjects)
        {
            MobSpawner ms = g.GetComponent<MobSpawner>();
            this.spawners.Add(ms);
        }
    }

    protected override void update()
    {
        foreach(MobSpawner ms in this.spawners)
        {
            if (!ms.spawnerCleared())
            {
                return;
            }
        }

        this.removeBarriers();
    }
}
