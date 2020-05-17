using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AShopItem : ScriptableObject
{
    [SerializeField]
    protected Sprite icon;
    [SerializeField]
    protected string itemName;
    [SerializeField]
    protected int price;

    //Do something when purchased
    //Returns whether or not this item is still available
    public abstract bool onPurchase();

    public virtual Sprite getIcon()
    {
        return this.icon;
    }

    public virtual string getName()
    {
        return this.itemName;
    }

    public virtual int getPrice()
    {
        return this.price;
    }
}
