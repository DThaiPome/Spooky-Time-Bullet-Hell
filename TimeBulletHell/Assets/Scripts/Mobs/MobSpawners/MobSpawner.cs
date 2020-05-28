using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobSpawner : MonoBehaviour
{
    protected List<MobBehaviour> activeMobs;

    void Start()
    {
        this.start();
    }

    protected virtual void start()
    {
        this.activeMobs = new List<MobBehaviour>();
    }

    void OnEnable()
    {
        this.onEnable();
    }

    protected virtual void onEnable()
    {
        this.spawnIfReady();
    }

    void Update()
    {
        this.update();
    }

    protected virtual void update()
    {
        this.updateActiveList();
        this.spawnIfReady();
    }

    private void spawnIfReady()
    {
        this.updateActiveList();
        if (this.readyToSpawn())
        {
            foreach (MobBehaviour mb in this.spawnMobs())
            {
                this.activeMobs.Add(mb);
            }
        }
    }

    protected abstract bool readyToSpawn();

    protected abstract List<MobBehaviour> spawnMobs();

    protected virtual void updateActiveList()
    {
        for(int i = 0; i < this.activeMobs.Count; i++)
        {
            MobBehaviour mb = this.activeMobs[i];
            if (!mb.gameObject.activeSelf)
            {
                this.activeMobs.RemoveAt(i);
                i--;
            }
        }
    }
    void OnDisable()
    {
        this.onDisable();
    }

    protected virtual void onDisable()
    {
        this.resetRoom();
    }

    protected abstract void resetRoom();

    //For opening barriers and stuff - is everything defeated.
    //Infinite spawners should always return true for this.
    public abstract bool spawnerCleared();
}
