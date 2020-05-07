using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnEnable : MobSpawner
{
    [SerializeField]
    protected string mobName;
    [SerializeField]
    protected bool singularMob;

    protected bool spawned;

    protected override void onEnable()
    {
        base.onEnable();
        if (!this.singularMob)
        {
            this.spawned = false;
        }
    }

    protected override bool readyToSpawn()
    {
        return !this.spawned;
    }

    protected override void resetRoom()
    {
        foreach(MobBehaviour mb in this.activeMobs)
        {
            if (this.singularMob)
            {
                mb.transform.position = this.transform.position;
            }
            else
            {
                mb.gameObject.SetActive(false);
            }
        }
        this.activeMobs = new List<MobBehaviour>();
    }

    protected override List<MobBehaviour> spawnMobs()
    {
        this.spawned = true;
        return new List<MobBehaviour>() { MobManager.instance.spawn(this.mobName, new DefaultSpawnProperties(this.transform.position)) };
    }
}
