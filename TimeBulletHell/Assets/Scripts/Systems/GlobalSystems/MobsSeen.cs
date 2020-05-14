using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsSeen : MonoBehaviour
{
    [SerializeField]
    private List<string> mobsSeen;

    public static MobsSeen instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void seeMob(string name)
    {
        if (!this.mobsSeen.Contains(name))
        {
            this.mobsSeen.Add(name);
        }
    }

    public bool seenMob(string name)
    {
        return this.mobsSeen.Contains(name);
    }
}
