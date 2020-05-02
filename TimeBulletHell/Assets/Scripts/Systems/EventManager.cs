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
    //On player bullet hit
    public event Action<Transform, PlayerBullet> onPlayerBulletHitEvent;
    //On mob bullet hit
    public event Action<Transform, BulletBehaviour> onBulletHitEvent;
    //When a mob bullet hits a player
    public event Action<PlayerHitbox, BulletBehaviour> onBulletHitsPlayerEvent;
    //Normal player hurt
    public event Action onPlayerHurtEvent;

    //** INVENTORY ADDED
    //Pickup collected
    public event Action<APickup> onPickupCollectedEvent;
    //Add to inventory
    public event Action<InventoryItem> addToInventoryEvent;

    //** PLAYER EVEntS
    //Neutral drop collected
    public event Action onNeutralDropPickupEvent;

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

    public void onBulletHit(Transform t, BulletBehaviour bb)
    {
        if (onBulletHitEvent != null)
        {
            onBulletHitEvent(t, bb);
        }
    }

    public void onBulletHitsPlayer(PlayerHitbox ph, BulletBehaviour bb)
    {
        if (onBulletHitsPlayerEvent != null)
        {
            onBulletHitsPlayerEvent(ph, bb);
        }
    }

    public void onPlayerHurt()
    {
        if (onPlayerHurtEvent != null)
        {
            onPlayerHurtEvent();
        }
    }

    public void onPickupCollected(APickup ap)
    {
        if (onPickupCollectedEvent != null)
        {
            onPickupCollectedEvent(ap);
        }
    }

    public void addToInventory(InventoryItem ii)
    {
        if (addToInventoryEvent != null)
        {
            addToInventoryEvent(ii);
        }
    }

    public void onNeutralDropPickup()
    {
        if (onNeutralDropPickupEvent != null)
        {
            onNeutralDropPickupEvent();
        }
    }
}
