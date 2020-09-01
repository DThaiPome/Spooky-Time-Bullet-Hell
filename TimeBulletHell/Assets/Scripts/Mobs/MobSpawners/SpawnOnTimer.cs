using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnTimer : MobSpawner
{
    [SerializeField]
    protected string mobName;
    [SerializeField]
    private float secondsPerSpawn = 1;

    [SerializeField]
    private bool spawnInitialMobs = true;
    [SerializeField]
    private float mobSpeed = 3;
    [SerializeField]
    private float initialSpawnLineLength = 15;

    private bool initialLineSpawned;

    private float timeElapsed;

    protected override void onEnable()
    {
        base.onEnable();
        this.timeElapsed = this.secondsPerSpawn;
        this.initialLineSpawned = false;
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
        return this.spawnInitialMobs && !this.initialLineSpawned;
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
        List<MobBehaviour> spawned = new List<MobBehaviour>();

        if (this.spawnInitialMobs && !this.initialLineSpawned)
        {
            this.SpawnInitialLine(spawned);
        }

        spawned.Add(this.spawn(this.transform.position));
        return spawned;
    }

    private void SpawnInitialLine(List<MobBehaviour> spawnedList)
    {
        this.initialLineSpawned = true;

        float step = this.mobSpeed * this.secondsPerSpawn;
        int count = (int)(this.initialSpawnLineLength / step);

        Vector2 spawnLocation = (Vector2)this.transform.position;

        for(int i = 0; i < count; i++)
        {
            spawnLocation += step * (Vector2)this.transform.right;
            spawnedList.Add(this.spawn(spawnLocation));
        }
    }

    private MobBehaviour spawn(Vector2 position)
    {
        return MobManager.instance.spawn(this.mobName,
        new MobSpawnPropertiesWithRotation(position, this.transform.rotation));
    }
}
