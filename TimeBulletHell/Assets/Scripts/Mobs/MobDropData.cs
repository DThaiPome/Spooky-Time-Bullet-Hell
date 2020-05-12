using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MobDropData
{
    [SerializeField]
    public float frequency;
    [SerializeField]
    private List<string> pickups;
    
    public MobDropData(float frequency, List<string> pickups)
    {
        this.frequency = frequency;
        this.pickups = pickups;
    }

    public float getFrequency()
    {
        return frequency;
    }

    public void spawnPickups(Vector3 position)
    {
        foreach(string s in this.pickups)
        {
            float angle = UnityEngine.Random.value * 2 * Mathf.PI;
            float distance = UnityEngine.Random.value;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            offset *= distance;
            PickupManager.instance.spawn(s, new DefaultPickupSpawnProperties((Vector3)offset + position));
        }
    }
}
