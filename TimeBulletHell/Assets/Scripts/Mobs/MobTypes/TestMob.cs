using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : MobBehaviour
{
    [SerializeField]
    protected int bulletsPerSecond;
    [SerializeField]
    protected int bulletCount;
    [SerializeField]
    protected int bulletSpeed;
    [SerializeField]
    protected int sprayAngle;

    protected PlayerMovement pm;

    protected float secondsPerBullet;
    protected float timeElapsed;

    protected override void onEnable()
    {
        base.onEnable();
        this.secondsPerBullet = 1 / this.bulletsPerSecond;
        this.timeElapsed = secondsPerBullet / 2;
    }

    protected override void start()
    {
        base.start();
        this.pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
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
        Vector2 mobToPlayer = this.pm.transform.position - this.transform.position;
        float direction = Mathf.Atan2(mobToPlayer.y, mobToPlayer.x) * Mathf.Rad2Deg;

        float minAngle = this.bulletCount > 1 ? -this.sprayAngle / 2 : 0;
        float dAngle = this.bulletCount > 1 ? this.sprayAngle / (this.bulletCount - 1) : -minAngle;

        for (int i = 0; i < this.bulletCount; i++)
        {
            BulletManager.instance.fire("BasicBullet", new DefaultFireProperties(this.transform.position, direction + minAngle + (i * dAngle), this.bulletSpeed));
        }
    }
}
