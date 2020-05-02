using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletBehaviour
{
    [SerializeField]
    private int power;

    private MobBehaviour mobHit;

    protected override void start()
    {
        base.start();
    }

    protected override void onEnable()
    {
        base.onEnable();
        this.mobHit = null;
    }

    protected override void onTriggerEnter2D(Collider2D other)
    {
        EventManager.instance.onPlayerBulletHit(other.transform, this);
        this.gameObject.SetActive(false);
    }

    public virtual void damageMob(MobBehaviour mb)
    {
        if (this.mobHit == null)
        {
            this.mobHit = mb;
            this.hurt(this.mobHit);
        }
    }

    protected virtual void hurt(MobBehaviour mb)
    {
        mb.hurt(this.power);
        this.gameObject.SetActive(false);
    }
}
