using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelessGun : AGunWeapon
{
    protected string bulletType = "PlayerTimelessBullet";
    protected int bulletCount = 1;
    protected float bulletSpeed = 30;
    protected float sprayAngle = 0;

    public TimelessGun() : base(8)
    {
        this.ammo = 15;
    }

    protected override void fire()
    {
        float minAngle = this.bulletCount > 1 ? -this.sprayAngle / 2 : 0;
        float dAngle = this.bulletCount > 1 ? this.sprayAngle / (this.bulletCount - 1) : -minAngle;

        for (int i = 0; i < this.bulletCount; i++)
        {
            this.ammo--;
            if (ammo > 0)
            {
                BulletManager.instance.spawn(this.bulletType, new DefaultFireProperties(this.player.transform.position, this.player.transform.localEulerAngles.z + minAngle + (i * dAngle), this.bulletSpeed));
            }
        }
    }

    public override void update()
    {
        this.timeElapsed += Time.deltaTime;
        this.timeElapsed = Mathf.Min(this.timeElapsed, this.secondsPerShot);
        this.readyToFire = this.timeElapsed >= this.secondsPerShot;
    }
}
