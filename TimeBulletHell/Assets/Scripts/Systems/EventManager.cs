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

    //** ROOM EVENTS
    //On room change
    public event Action<string> onRoomChangeEvent;
    //Just set a room active
    public event Action<string> setRoomActiveEvent;

    //** BULLET EVENTS
    //On player bullet hit
    public event Action<Transform, PlayerBullet> onPlayerBulletHitEvent;
    //On mob bullet hit
    public event Action<Transform, BulletBehaviour> onBulletHitEvent;
    //When a mob bullet hits a player
    public event Action<PlayerHitbox, BulletBehaviour> onBulletHitsPlayerEvent;

    //** INVENTORY ADDED
    //Pickup collected
    public event Action<APickup> onPickupCollectedEvent;
    //Add to inventory
    public event Action<InventoryItem> addToInventoryEvent;

    //** PLAYER EVEntS
    //Neutral drop collected
    public event Action onNeutralDropPickupEvent;
    //Attempt the damage the player
    public event Action hurtPlayerEvent;
    //Normal player hurt
    public event Action onPlayerHurtEvent;
    //Control Mode Changed
    public event Action<string> onPlayerControlModeChangedEvent;

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

    public void hurtPlayer()
    {
        if (hurtPlayerEvent != null)
        {
            hurtPlayerEvent();
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

    public void onRoomChange(string id)
    {
        if (onRoomChangeEvent != null)
        {
            onRoomChangeEvent(id);
        }
    }

    public void onPlayerControlModeChanged(string mode)
    {
        if (onPlayerControlModeChangedEvent != null)
        {
            onPlayerControlModeChangedEvent(mode);
        }
    }

    public void setRoomActive(string id)
    {
        if (setRoomActiveEvent != null)
        {
            setRoomActiveEvent(id);
        }
    }
}
