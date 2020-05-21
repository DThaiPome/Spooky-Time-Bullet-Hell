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

    protected bool available = true;

    public virtual bool tryToPurchase()
    {
        if (this.ableToPurchase())
        {
            this.purchaseIfAffordable();
        }

        return this.available;
    }

    protected virtual void purchaseIfAffordable()
    {
        EventManager.instance.returnPointCountGreaterOrEqualEvent += this.returnAffordable;
        EventManager.instance.returnPointCountLessEvent += this.returnUnaffordable;
        EventManager.instance.queryPointsCount(this, this.price);
    }

    protected virtual void returnAffordable(System.Object o)
    {
        if (o.Equals(this))
        {
            this.onPurchase();
            EventManager.instance.returnPointCountGreaterOrEqualEvent -= this.returnAffordable;
            EventManager.instance.returnPointCountLessEvent -= this.returnUnaffordable;
        }
    }

    protected virtual void returnUnaffordable(System.Object o)
    {
        if (o.Equals(this))
        {
            EventManager.instance.returnPointCountGreaterOrEqualEvent -= this.returnAffordable;
            EventManager.instance.returnPointCountLessEvent -= this.returnUnaffordable;
        }
    }


    //Do something when purchased
    //Returns whether or not this item is still available
    protected virtual void onPurchase()
    {
        EventManager.instance.onItemPurchase(this);
    }

    //Check if the item is purchaseable
    protected abstract bool ableToPurchase();

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
