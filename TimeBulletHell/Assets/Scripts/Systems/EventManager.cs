﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    //** TIME EVENTS
    public event Action onWarpedTickEvent;
    public event Action onFixedWarpedTickEvent;

    //** UI EVENTS
    //On Click
    public event Action<Transform> onClickEvent;
    //On Hover Enter
    public event Action<Transform> onHoverEnterEvent;
    //On Hover Exst
    public event Action<Transform> onHoverExitEvent;
    //** LEVEL SELECT MENU EVENTS
    //On arrow click
    public event Action<LevelSelectArrowDirection> onLevelSelectArrowClickedEvent;
    //On level selected
    public event Action<LevelInfo> onLevelSelectedEvent;
    //On start menu back button clicked
    public event Action onStartViewBackButtonClickedEvent;
    //On item purchased
    public event Action<int> onItemPurchasedEvent;

    //** ROOM EVENTS
    //Make a room change
    public event Action<string> switchToRoomEvent;
    //On a room change
    public event Action<RoomObject> onRoomChangeEvent;
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
    //Inventory Pickup collected
    public event Action<InventoryPickup> onInventoryItemCollectedEvent;
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
    public event Action<PlayerControlMode> onPlayerControlModeChangedEvent;

    //** LEVEL EVENTS
    //Boss defeated
    public event Action onBossDefeatedEvent;
    //End the level
    public event Action endLevelEvent;
    //Switch to the level
    public event Action<string> switchToLevelEvent;
    //On level switched
    public event Action<string> onLevelSwitchedEvent;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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

    public void onInventoryItemCollected(InventoryPickup ap)
    {
        if (onInventoryItemCollectedEvent != null)
        {
            onInventoryItemCollectedEvent(ap);
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

    public void switchToRoom(string id)
    {
        if (switchToRoomEvent != null)
        {
            switchToRoomEvent(id);
        }
    }

    public void onPlayerControlModeChanged(PlayerControlMode mode)
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

    public void onRoomChange(RoomObject ro)
    {
        if (onRoomChangeEvent != null)
        {
            onRoomChangeEvent(ro);
        }
    }

    public void onBossDefeated()
    {
        if (onBossDefeatedEvent != null)
        {
            onBossDefeatedEvent();
        }
    }

    public void endLevel()
    {
        if (endLevelEvent != null)
        {
            endLevelEvent();
        }
    }

    public void switchToLevel(string level)
    {
        if (switchToLevelEvent != null)
        {
            switchToLevelEvent(level);
        }
    }

    public void onLevelSwitch(string level)
    {
        if (onLevelSwitchedEvent != null)
        {
            onLevelSwitchedEvent(level);
        }
    }

    public void onClick(Transform t)
    {
        if (onClickEvent != null)
        {
            onClickEvent(t);
        }
    }

    public void onHoverEnter(Transform t)
    {
        if (onHoverEnterEvent != null)
        {
            onHoverEnterEvent(t);
        }
    }

    public void onHoverExit(Transform t)
    {
        if (onHoverExitEvent != null)
        {
            onHoverExitEvent(t);
        }
    }

    public void onLevelSelectArrowClicked(LevelSelectArrowDirection direction)
    {
        if (onLevelSelectArrowClickedEvent != null)
        {
            onLevelSelectArrowClickedEvent(direction);
        }
    }

    public void onLevelSelected(LevelInfo info)
    {
        if (onLevelSelectedEvent != null)
        {
            {
                onLevelSelectedEvent(info);
            }
        }
    }

    public void onStartViewBackButtonClicked()
    {
        if (onStartViewBackButtonClickedEvent != null)
        {
            onStartViewBackButtonClickedEvent();
        }
    }

    public void onItemPurchased(int price)
    {
        if (onItemPurchasedEvent != null)
        {
            onItemPurchasedEvent(price);
        }
    }
}
