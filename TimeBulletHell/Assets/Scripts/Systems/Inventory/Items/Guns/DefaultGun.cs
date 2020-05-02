using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : AGunWeapon
{
    protected string bulletType = "PlayerBullet";
    protected int bulletCount = 1;
    protected float bulletSpeed = 30;
    protected float sprayAngle = 0;

    public DefaultGun() : base(8) { }

    protected override void fire()
    {
        float minAngle = this.bulletCount > 1 ? -this.sprayAngle / 2 : 0;
        float dAngle = this.bulletCount > 1 ? this.sprayAngle / (this.bulletCount - 1) : -minAngle;

        for (int i = 0; i < this.bulletCount; i++)
        {
            BulletManager.instance.fire(this.bulletType, new DefaultFireProperties(this.player.transform.position, this.player.transform.localEulerAngles.z + minAngle + (i * dAngle), this.bulletSpeed));
        }
    }

    public override bool shouldDiscard()
    {
        return false;
    }
}
