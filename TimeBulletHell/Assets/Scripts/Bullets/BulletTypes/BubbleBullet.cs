using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBullet : BulletBehaviour
{
    protected Vector2 origin;
    protected float duration;
    protected string burstType;
    protected float burstCount;
    protected float burstSpeed;

    private float timeElapsed;
    private float initialSpeed;

    protected override void start()
    {
        base.start();
        EventManager.instance.onPlayerBulletHitEvent += this.onPlayerBulletHit;
    }
    public void fire(Vector3 origin, float direction, float speed, float duration, string burstType, float burstCount, float burstSpeed)
    {
        this.fire(origin, direction, speed);
        this.duration = duration;
        this.origin = origin;
        this.burstType = burstType;
        this.burstCount = burstCount;
        this.burstSpeed = burstSpeed;
        this.timeElapsed = 0;
        this.initialSpeed = speed;
    }

    protected override void move()
    {
        base.move();
        this.timeElapsed += GameTime.instance.fixedDeltaTime();
        this.speed -= (this.initialSpeed / this.duration) * GameTime.instance.fixedDeltaTime();
        if (this.timeElapsed >= this.duration)
        {
            this.burst();
            this.gameObject.SetActive(false);
        }
    }

    protected override void onTriggerEnter2D(Collider2D other)
    {
        base.onTriggerEnter2D(other);
        if (other.tag == "Player")
        {
            this.burst();
        }
    }

    protected virtual void onPlayerBulletHit(Transform t, PlayerBullet pb)
    {
        if (t.Equals(this.transform))
        {
            this.gameObject.SetActive(false);
            this.burst();
            pb.gameObject.SetActive(false);
        }
    }

    private void burst()
    {
        float diff = 360 / this.burstCount;
        for(int i = 0; i < this.burstCount; i++)
        {
            BulletManager.instance.spawn(this.burstType, new DefaultFireProperties(this.transform.position, diff * (i + 0.5f), this.burstSpeed));
        }
    }

    protected override void disableIfReady()
    {
        
    }
}

public class BubbleBulletFireProperties : DefaultFireProperties
{
    protected float duration;
    protected string burstType;
    protected float burstCount;
    protected float burstSpeed;

    public BubbleBulletFireProperties(Vector2 origin, float direction, float speed, float distance, string burstType, float burstCount, float burstSpeed) 
        : base(origin, direction, speed)
    {
        this.duration = distance;
        this.burstType = burstType;
        this.burstCount = burstCount;
        this.burstSpeed = burstSpeed;
    }

    public override void spawn(BulletBehaviour bb)
    {
        BubbleBullet bubble = null;
        try
        {
            bubble = (BubbleBullet)bb;
        }
        catch (Exception e) { }
        if (bubble != null)
        {
            bubble.fire(this.origin, this.direction, this.speed, this.duration, this.burstType, this.burstCount, this.burstSpeed);
        }
    }
}
