using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletBehaviour
{
    [SerializeField]
    private int power;

    protected override void onTriggerEnter2D(Collider2D other)
    {
        EventManager.instance.onPlayerBulletHit(other.transform, this);
    }

    public virtual void damageMob(MobBehaviour mb)
    {
        mb.hurt(this.power);
        this.gameObject.SetActive(false);
    }
}
