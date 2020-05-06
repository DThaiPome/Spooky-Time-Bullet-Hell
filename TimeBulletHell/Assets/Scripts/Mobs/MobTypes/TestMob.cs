using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MobBehaviour
{
    [SerializeField]
    protected float bulletsPerSecond;
    [SerializeField]
    protected int bulletCount;
    [SerializeField]
    protected int bulletSpeed;
    [SerializeField]
    protected int sprayAngle;

    protected float secondsPerBullet;
    protected float timeElapsed;

    protected override void onEnable()
    {
        base.onEnable();
        this.secondsPerBullet = this.bulletsPerSecond == 0 ? 1 : 1 / this.bulletsPerSecond;
        this.timeElapsed = secondsPerBullet / 2;
    }

    protected override void update()
    {
        base.update();
        this.timeElapsed += GameTime.instance.deltaTime();
        while (this.timeElapsed >= this.secondsPerBullet)
        {
            this.fire();
            this.timeElapsed -= this.secondsPerBullet;
        }
    }

    protected virtual void fire()
    {
        float direction = this.angleToPlayer();

        float minAngle = this.bulletCount > 1 ? -this.sprayAngle / 2 : 0;
        float dAngle = this.bulletCount > 1 ? this.sprayAngle / (this.bulletCount - 1) : -minAngle;

        for (int i = 0; i < this.bulletCount; i++)
        {
            BulletManager.instance.spawn("BasicBullet", new DefaultFireProperties(this.transform.position, direction + minAngle + (i * dAngle), this.bulletSpeed));
        }
    }
}
