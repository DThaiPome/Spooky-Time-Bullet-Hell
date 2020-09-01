using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnTimer : MobSpawner
{
    [SerializeField]
    protected string mobName;
    [SerializeField]
    private float secondsPerSpawn = 1;

    private float timeElapsed;

    protected override void onEnable()
    {
        base.onEnable();
        this.timeElapsed = this.secondsPerSpawn;
    }

    public override bool spawnerCleared()
    {
        return false;
    }

    protected override void update()
    {
        base.update();
        this.timeElapsed += GameTime.instance.deltaTime();
    }

    protected override bool readyToSpawn()
    {
        if (this.timeElapsed >= this.secondsPerSpawn)
        {
            this.timeElapsed -= this.secondsPerSpawn;
            return true;
        }
        return false;
    }

    protected override void resetRoom()
    {
        foreach(MobBehaviour mb in this.activeMobs)
        {
            mb.gameObject.SetActive(false);
        }
        this.activeMobs = new List<MobBehaviour>();
    }

    protected override List<MobBehaviour> spawnMobs()
    {
        return new List<MobBehaviour>() {
            MobManager.instance.spawn(this.mobName, 
            new MobSpawnPropertiesWithRotation(this.transform.position, this.transform.rotation)) };
    }
}
