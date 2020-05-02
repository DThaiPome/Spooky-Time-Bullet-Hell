using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            this.onPickup();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            this.onPickup();
        }
    }

    public virtual void spawn(Vector2 origin)
    {
        this.transform.position = origin;
    }

    protected virtual void onPickup()
    {
        EventManager.instance.onPickupCollected(this);
    }

    public virtual void addToInventory()
    {
        EventManager.instance.addToInventory(this.getInventoryItem());
    }

    protected abstract InventoryItem getInventoryItem();
}

public class DefaultPickupSpawnProperties : IPickupSpawnProperties
{
    private Vector2 origin;

    public DefaultPickupSpawnProperties(Vector2 origin)
    {
        this.origin = origin;
    }

    public void spawn(APickup ap)
    {
        ap.spawn(this.origin);
    }
}
