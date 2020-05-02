using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AGunWeapon : InventoryItem
{
    protected int ammo;
    protected float shotsPerSecond;

    protected float secondsPerShot;
    protected float timeElapsed;
    protected bool readyToFire;
    protected GameObject player;

    public AGunWeapon(float shotsPerSecond)
    {
        this.player = GameObject.Find("Player");
        this.shotsPerSecond = shotsPerSecond;
        this.secondsPerShot = 1 / this.shotsPerSecond;
    }

    public virtual bool shouldDiscard()
    {
        return ammo <= 0;
    }

    public virtual void update()
    {
        this.timeElapsed += GameTime.instance.deltaTime();
        this.timeElapsed = Mathf.Min(this.timeElapsed, this.secondsPerShot);
        this.readyToFire = this.timeElapsed >= this.secondsPerShot;
    }

    public virtual void use()
    {
        if (this.readyToFire)
        {
            this.timeElapsed -= this.secondsPerShot;
            this.fire();
        }
    }

    protected abstract void fire();

    public virtual bool usedContinuously()
    {
        return true;
    }
}
