using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    //Time events
    public event Action onWarpedTickEvent;
    public event Action onFixedWarpedTickEvent;

    void Awake()
    {
        instance = this;
    }

    public void onWarpedTick()
    {
        if (onWarpedTickEvent != null)
        {
            onWarpedTickEvent();
        }
    }

    public void onFixedWarpedTick()
    {
        if (onFixedWarpedTickEvent != null)
        {
            onFixedWarpedTickEvent();
        }
    }
}
