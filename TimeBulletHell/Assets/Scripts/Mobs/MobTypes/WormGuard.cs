using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormGuard : MobBehaviour
{
    [SerializeField]
    private string bulletType = "BasicBullet";
    [SerializeField]
    private float bulletSpeed = 6;
    [SerializeField]
    private int bulletCount = 4;

    [SerializeField]
    private float arcAngle = 45;
    [SerializeField]
    private int arcCount = 10;
    [SerializeField]
    private float arcDuration = .9f;
    [SerializeField]
    private float arcRestSeconds = .25f;

    private float secondsPerArc;
    private float arcTimeElapsed;
    private float arcsFired;
    private bool reducedArc;

    private float arcAngleMultiplier;
    private bool resting;
    private float restingTimeElapsed;

    protected override void onEnable()
    {
        base.onEnable();
        this.secondsPerArc = this.arcDuration / (float)this.arcCount;
        this.arcTimeElapsed = 0;
        this.arcsFired = 0;
        this.reducedArc = false;

        this.arcAngleMultiplier = -1;
        this.resting = false;
    }

    public override void hurt(int damage)
    {
        base.hurt(damage);
    }

    protected override void update()
    {
        base.update();
        if (this.resting)
        {
            this.RestingUpdate();
        } else
        {
            this.FiringUpdate();
        }
    }

    private void RestingUpdate()
    {
        this.restingTimeElapsed += GameTime.instance.deltaTime();
        if (this.restingTimeElapsed >= this.arcRestSeconds)
        {
            this.restingTimeElapsed = 0;
            this.resting = false;
        }
    }

    private void FiringUpdate()
    {
        if (this.arcsFired < this.arcCount)
        {
            this.arcTimeElapsed += GameTime.instance.deltaTime();
            while (this.arcTimeElapsed >= this.secondsPerArc)
            {
                if (this.arcsFired < this.arcCount)
                {
                    this.arcTimeElapsed -= this.secondsPerArc;
                    this.arcsFired++;
                    if (this.reducedArc)
                    {
                        this.FireArc(0.5f, this.bulletCount - 1);
                    } else
                    {
                        this.FireArc(0, this.bulletCount);
                    }
                    this.reducedArc = !this.reducedArc;
                }
            }
        } else
        {
            this.arcTimeElapsed = 0;
            this.arcsFired = 0;
            this.arcAngleMultiplier *= -1;
            this.reducedArc = false;
            this.resting = true;
        }
    }

    private void FireArc(float stepOffset, int bulletCount)
    {
        float step = this.arcAngle / (this.bulletCount * this.arcAngleMultiplier);
        float angle = this.transform.eulerAngles.z + (stepOffset * step);
        for(int i = 0; i < bulletCount; i++)
        {
            this.Fire(angle);
            angle += step;
        }
    }
    
    private void Fire(float angle)
    {
        BulletManager.instance.spawn(this.bulletType, new DefaultFireProperties(this.transform.position, angle, this.bulletSpeed));
    }
}
