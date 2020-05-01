using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    //** TIME EVENTS
    public event Action onWarpedTickEvent;
    public event Action onFixedWarpedTickEvent;

    //** BULLET EVENTS
    //On a bullet collision
    public event Action<Transform, PlayerBullet> onPlayerBulletHitEvent;

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

    public void onPlayerBulletHit(Transform t, PlayerBullet pb)
    {
        if (onPlayerBulletHitEvent != null)
        {
            onPlayerBulletHitEvent(t, pb);
        }
    }
}
