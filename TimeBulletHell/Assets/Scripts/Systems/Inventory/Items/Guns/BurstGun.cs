using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstGun : AGunWeapon
{
    protected string burstBulletType = "PlayerBullet";
    protected int burstCount = 2;
    protected float burstSpeed = 3;
    protected float burstDuration = 1.5f;
    protected int burstSprayAngle = 45;
    protected int burstBulletCount = 6;
    protected int burstBulletSpeed = 2;

    public BurstGun() : base(3)
    {
        this.ammo = 20;
    }

    protected override void fire()
    {
        float minAngle = this.burstCount > 1 ? -this.burstSprayAngle / 2 : 0;
        float dAngle = this.burstCount > 1 ? this.burstSprayAngle / (this.burstCount - 1) : -minAngle;

        for (int i = 0; i < this.burstCount; i++)
        {
            this.ammo--;
            BulletManager.instance.spawn("PlayerBubbleBullet", new BubbleBulletFireProperties(PlayerInfoInGame.instance.getPosition(), PlayerInfoInGame.instance.getEulerAngles().z + minAngle + (i * dAngle), this.burstSpeed, this.burstDuration, this.burstBulletType, this.burstBulletCount, this.burstBulletSpeed));
        }
    }
}
